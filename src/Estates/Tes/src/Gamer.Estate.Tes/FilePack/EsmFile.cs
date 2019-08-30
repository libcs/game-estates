using Gamer.Core;
using Gamer.Estate.Tes.Records;
using Gamer.Proxy;
using System;
using System.IO;

namespace Gamer.Estate.Tes.FilePack
{
    public partial class EsmFile : RecordGroup, IDisposable
    {
        const int recordHeaderSizeInBytes = 16;
        public override string ToString() => Path.GetFileName(FilePath);

        public EsmFile(ProxySink proxySink, string filePath, TesGame game)
            : base(proxySink, !string.IsNullOrEmpty(filePath) ? new BinaryFileReader(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)) : null, filePath, ToFormat(game), 1, 0)
        {
            //void process()
            //{
            //    var watch = new Stopwatch(); watch.Start();
            //    Core.Debug.Log($"Loading: {watch.ElapsedMilliseconds}");
            //    Process();
            //    watch.Stop();
            //}
            if (_proxySink is ProxySinkServer)
                return;
            if (_proxySink is ProxySinkClient)
            {
                SinkDataContains(null);
                Process();
                return;
            }
            if (filePath == null)
                return;
            Read();
            Process();
        }

        public void Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }
        ~EsmFile() => Close();

        public void SinkDataContains(string path)
        {
            var bytes = _proxySink.GetDataContains(() =>
            {
                var info = new ProxySink.DataInfo();
                Read(info);
                return info.ToArray();
            });
            if (_proxySink is ProxySinkServer)
                return;
            // write data
            if (path != null)
                File.WriteAllBytes(Path.Combine(path, ".set"), bytes);
            ReadGRUP(new ProxySink.DataInfo(bytes), path);
        }

        void Read(ProxySink.DataInfo info = null)
        {
            Clear();
            var rootHeader = new Header(null, _r, Format);
            if ((Format == GameFormat.TES3 && rootHeader.Type != "TES3") || (Format != GameFormat.TES3 && rootHeader.Type != "TES4"))
                throw new FormatException($"{FilePath} record header {rootHeader.Type} is not valid for this {Format}");
            var rootRecord = rootHeader.CreateRecord(rootHeader.Position, RecordLevel);
            rootRecord.Read(_r, FilePath, Format);
            // morrowind hack
            if (Format == GameFormat.TES3)
            {
                var header = new Header { Type = "ALL_", Label = string.Empty, DataSize = (uint)(_r.BaseStream.Length - _r.Position), Position = _r.Position };
                ReadTES3(header, true, info);
                return;
            }
            // read groups
            var endPosition = _r.BaseStream.Length;
            while (_r.Position < endPosition)
            {
                var header = new Header(this, _r, Format);
                if (header.Type != "GRUP")
                    throw new InvalidOperationException($"{header.Type} not GRUP");
                ReadGRUP(header, false, info);
            }
        }
    }
}
