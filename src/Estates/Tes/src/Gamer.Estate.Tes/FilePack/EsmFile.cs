using Gamer.Core;
using Gamer.Estate.Tes.Records;
using Gamer.Proxy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Dictionary<string, RecordGroup> GroupByLabel;

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
            FilePath = filePath;
            Format = format();
            _proxySink = proxySink;
            if (proxySink == null || proxySink is ProxySinkServer)
                return;
            if (proxySink is ProxySinkClient)
            {
                SinkDataContains();
                return;
            }
            if (filePath == null)
                return;
            //var watch = new Stopwatch(); watch.Start();
            Read(File.Open(FilePath, FileMode.Open, FileAccess.Read));
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

        public void ExportDataPack(string path)
        {
            var info = new ProxySink.DataInfo();
            Read(File.Open(FilePath, FileMode.Open, FileAccess.Read), info);
            //    foreach (var group in Groups)
            //        foreach (var image in group.Value.GetImages())
            //            File.WriteAllBytes(Path.Combine(splitPath, image.Item1 + ".dat"), image.Item2);
        }

        public void SinkDataContains()
        {
            var info = new ProxySink.DataInfo(_proxySink.GetDataContains(() =>
            {
                var info2 = new ProxySink.DataInfo();
                Read(File.Open(FilePath, FileMode.Open, FileAccess.Read), info: info2);
                return info2.ToArray();
            }));
            //var state = GroupByLabel; RecordGroup last = null;
            //var stack = new Stack<(List<RecordGroup> state, RecordGroup last)>();
            //info.Decoder(
            //    header: (l, b) =>
            //    {
            //        var label = Encoding.ASCII.GetString(l);
            //        if (!state.TryGetValue(label, out var group))
            //        {
            //            group = new RecordGroup(null, FilePath, Format, 0);
            //            state.Add(label, group);
            //        }
            //        last = group;
            //    },
            //    headerPush: () => { stack.Push((a: state, b: last)); state = last.Groups; },
            //    headerPop: () => { var (a, b) = stack.Pop(); state = a; last = b; }
            //);
        }

        public Task<byte[]> LoadDataLabelAsync(byte[] label) => _proxySink.LoadDataLabelAsync(label, () =>
        {
            return null;
        });

        void Read(Stream input, ProxySink.DataInfo info = null)
        {
            var recordLevel = 1;
            if (_r != null)
                throw new InvalidOperationException("Attempt to re-open existing reader");
            _r = new BinaryFileReader(input);
            var rootHeader = new Header(_r, Format, null);
            if ((Format == GameFormat.TES3 && rootHeader.Type != "TES3") || (Format != GameFormat.TES3 && rootHeader.Type != "TES4"))
                throw new FormatException($"{FilePath} record header {rootHeader.Type} is not valid for this {Format}");
            var rootRecord = rootHeader.CreateRecord(rootHeader.Position, recordLevel);
            rootRecord.Read(_r, FilePath, Format);
            // morrowind hack
            if (Format == GameFormat.TES3)
            {
                var group = new RecordGroup(_r, FilePath, Format, recordLevel);
                var header = new Header
                {
                    Label = null,
                    DataSize = (uint)(_r.BaseStream.Length - _r.Position),
                    Position = _r.Position,
                };
                group.AddHeader(header, info);
                info?.AddHeader(header.Label, header.Position);
                group.Load(true, info);
                GroupByLabel = group.Records.GroupBy(x => x.Header.Type)
                    .ToDictionary(x => x.Key, x =>
                    {
                        var s = new RecordGroup(_r, FilePath, Format, recordLevel) { Records = x.ToList() };
                        s.AddHeader(new Header { Label = Encoding.ASCII.GetBytes(x.Key) }, null);
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
                var nextPosition = _r.Position + header.DataSize;
                var label = Encoding.ASCII.GetString(header.Label);
                if (!GroupByLabel.TryGetValue(label, out var group))
                    GroupByLabel.Add(label, group = new RecordGroup(_r, FilePath, Format, recordLevel));
                group.AddHeader(header, info);
                info?.AddHeader(header.Label, header.Position);
                if (info != null)
                    group.Load(info: info);
                _r.Position = nextPosition;
            }
        }
    }
}