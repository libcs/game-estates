using Gamer.Core;
using Gamer.Estate.Tes.Records;
using Gamer.Proxy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Gamer.Estate.Tes.FilePack
{
    public partial class EsmFile : IDisposable
    {
        const int recordHeaderSizeInBytes = 16;
        public override string ToString() => Path.GetFileName(FilePath);
        readonly ProxySink _proxySink;
        BinaryFileReader _r;
        public string FilePath;
        public GameFormat Format;
        public Dictionary<string, RecordGroup> Groups;

        public EsmFile(ProxySink proxySink, string filePath, TesGame game)
        {
            GameFormat format()
            {
                switch (game)
                {
                    // tes
                    case TesGame.Morrowind: return GameFormat.TES3;
                    case TesGame.Oblivion: return GameFormat.TES4;
                    case TesGame.Skyrim:
                    case TesGame.SkyrimSE:
                    case TesGame.SkyrimVR: return GameFormat.TES5;
                    // fallout
                    case TesGame.Fallout3:
                    case TesGame.FalloutNV: return GameFormat.TES4;
                    case TesGame.Fallout4:
                    case TesGame.Fallout4VR: return GameFormat.TES5;
                    default: throw new InvalidOperationException();
                }
            }

            if (filePath == null)
                return;
            FilePath = filePath;
            Format = format();
            _proxySink = proxySink;
            if (proxySink is ProxySinkClient)
                return;
            _r = new BinaryFileReader(File.Open(filePath, FileMode.Open, FileAccess.Read));
            //var watch = new Stopwatch(); watch.Start();
            Read(1);
            //Log($"Loading: {watch.ElapsedMilliseconds}");
            Process();
            //watch.Stop();
        }

        public void Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }
        ~EsmFile() => Close();

        public void Close()
        {
            _r?.Close();
            _r = null;
        }

        void Read(int recordLevel)
        {
            var rootHeader = new Header(_r, Format, null);
            if ((Format == GameFormat.TES3 && rootHeader.Type != "TES3") || (Format != GameFormat.TES3 && rootHeader.Type != "TES4"))
                throw new FormatException($"{FilePath} record header {rootHeader.Type} is not valid for this {Format}");
            var rootRecord = rootHeader.CreateRecord(rootHeader.Position, recordLevel);
            rootRecord.Read(_r, FilePath, Format);
            // morrowind hack
            if (Format == GameFormat.TES3)
            {
                var group = new RecordGroup(_r, FilePath, Format, recordLevel);
                group.AddHeader(new Header
                {
                    Label = null,
                    DataSize = (uint)(_r.BaseStream.Length - _r.Position),
                    Position = _r.Position,
                });
                group.Load();
                Groups = group.Records.GroupBy(x => x.Header.Type)
                    .ToDictionary(x => x.Key, x =>
                    {
                        var s = new RecordGroup(_r, FilePath, Format, recordLevel) { Records = x.ToList() };
                        s.AddHeader(new Header { Label = Encoding.ASCII.GetBytes(x.Key) });
                        return s;
                    });
                return;
            }
            // read groups
            Groups = new Dictionary<string, RecordGroup>();
            var endPosition = _r.BaseStream.Length;
            while (_r.Position < endPosition)
            {
                var header = new Header(_r, Format, null);
                if (header.Type != "GRUP")
                    throw new InvalidOperationException($"{header.Type} not GRUP");
                var nextPosition = _r.Position + header.DataSize;
                var label = Encoding.ASCII.GetString(header.Label);
                if (!Groups.TryGetValue(label, out RecordGroup group))
                {
                    group = new RecordGroup(_r, FilePath, Format, recordLevel);
                    Groups.Add(label, group);
                }
                group.AddHeader(header);
                _r.Position = nextPosition;
            }
        }
    }
}