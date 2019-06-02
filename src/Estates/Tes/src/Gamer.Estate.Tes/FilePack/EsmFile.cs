using Gamer.Core;
using Gamer.Estate.Tes.Records;
using Gamer.Proxy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Gamer.Estate.Tes.FilePack
{
    public partial class EsmFile : RecordGroup, IDisposable
    {
        const int recordHeaderSizeInBytes = 16;
        public override string ToString() => Path.GetFileName(FilePath);

        public EsmFile(ProxySink proxySink, string filePath, TesGame game)
            : base(proxySink, filePath != null ? new BinaryFileReader(File.Open(filePath, FileMode.Open, FileAccess.Read)) : null, filePath, ToFormat(game), 1)
        {
            void process()
            {
                var watch = new Stopwatch(); watch.Start();
                Core.Debug.Log($"Loading: {watch.ElapsedMilliseconds}");
                Process();
                watch.Stop();
            }
            if (_proxySink == null || _proxySink is ProxySinkServer)
                return;
            if (_proxySink is ProxySinkClient)
            {
                SinkDataContains(null);
                process();
                return;
            }
            if (filePath == null)
                return;
            Read();
            process();
        }

        public void Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }
        ~EsmFile() => Close();

        public void SinkDataContains(string path)
        {
            var data = _proxySink.GetDataContains(() =>
            {
                var info = new ProxySink.DataInfo();
                Read(info);
                return info.ToArray();
            });
            //    foreach (var group in Groups)
            //        foreach (var image in group.Value.GetImages())
            //            File.WriteAllBytes(Path.Combine(splitPath, image.Item1 + ".dat"), image.Item2);
            if (_proxySink is ProxySinkClient)
                ReadGRUP(new ProxySink.DataInfo(data));
        }

        void Read(ProxySink.DataInfo info = null)
        {
            var rootHeader = new Header(_r, Format, null);
            if ((Format == GameFormat.TES3 && rootHeader.Type != "TES3") || (Format != GameFormat.TES3 && rootHeader.Type != "TES4"))
                throw new FormatException($"{FilePath} record header {rootHeader.Type} is not valid for this {Format}");
            var rootRecord = rootHeader.CreateRecord(rootHeader.Position, RecordLevel);
            rootRecord.Read(_r, FilePath, Format);
            // morrowind hack
            if (Format == GameFormat.TES3)
            {
                var header = new Header { Label = null, DataSize = (uint)(_r.BaseStream.Length - _r.Position), Position = _r.Position };
                info?.AddGroup(header.Label, header.Position, null);
                var group = new RecordGroup(_proxySink, _r, FilePath, Format, RecordLevel);
                group.AddHeader(header, info);
                group.Load(true, info);
                GroupByLabel = group.Records.GroupBy(x => x.Header.Type)
                    .ToDictionary(x => x.Key, x =>
                    {
                        var s = new RecordGroup(_proxySink, _r, FilePath, Format, RecordLevel) { Records = x.ToList() };
                        s.AddHeader(new Header { Label = x.Key }, null);
                        return s;
                    });
                return;
            }
            // read groups
            GroupByLabel = new Dictionary<string, RecordGroup>();
            var endPosition = _r.BaseStream.Length;
            while (_r.Position < endPosition)
            {
                var header = new Header(_r, Format, null);
                if (header.Type != "GRUP")
                    throw new InvalidOperationException($"{header.Type} not GRUP");
                ReadGRUP(rootHeader, header, false, info);
            }
        }
    }
}
