using Game.Core;
using Game.Core.Netstream;
using Game.Estate.UltimaIX.Records;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Game.Core.CoreDebug;

//http://wiki.ultimacodex.com/wiki/Ultima_IX_Internal_Formats#FLX_Format
//https://github.com/cthielen/u9tools
namespace Game.Estate.UltimaIX.FilePack
{
    public class Header
    {
        public string Label;
        public byte State;
    }

    public partial class RecordGroup
    {
        public RecordGroup Next;
        public List<Header> Headers = new List<Header>();
        public List<Record> Records = new List<Record>();
        public Dictionary<string, RecordGroup> GroupByLabel;
        protected readonly StreamSink _streamSink;
        public readonly string FilePath;
        protected BinaryFileReader[] _r;
        public object Tag;

        public RecordGroup(StreamSink streamSink, string filePath)
        {
            _streamSink = streamSink;
            FilePath = filePath;
        }

        public void Clear()
        {
            Headers.Clear();
            Records.Clear();
        }

        public void AddHeader(Header header, StreamSink.DataInfo info)
        {
            //Log($"Read: {header.Label}");
            Headers.Add(header);
            if (info == null)
                switch (header.Label)
                {
                    case "9_": header.State = 1; Load(); break;
                }
        }

        public List<Record> Load(bool loadAll = false, StreamSink.DataInfo info = null)
        {
            foreach (var header in Headers.Where(x => x.State == 1))
            {
                header.State = 2;
                string path;
                _r = new[] {
                    File.Exists(path = Path.Combine(FilePath, $"static/fixed.{header.Label}")) ? new BinaryFileReader(File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read)) : null,
                    File.Exists(path = Path.Combine(FilePath, $"static/terrain.{header.Label}")) ? new BinaryFileReader(File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read)) : null,
                    File.Exists(path = Path.Combine(FilePath, $"runtime/nonfixed.{header.Label}")) ? new BinaryFileReader(File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read)) : null,
                };
                ReadGroup(header, loadAll, info);
            }
            return Records;
        }

        void ReadGroup(Header header, bool loadAll, StreamSink.DataInfo info)
        {
            if (GroupByLabel == null)
                GroupByLabel = new Dictionary<string, RecordGroup>();
            if (!GroupByLabel.TryGetValue(header.Label, out var group))
                GroupByLabel.Add(header.Label, group = new RecordGroup(_streamSink, FilePath));
            else group = new RecordGroup(_streamSink, FilePath) { Next = group };

            if (_r[0] != null) using (_r[0]) CELLRecord.ReadFixed(_r[0], header, group);
            if (_r[1] != null) using (_r[1]) LANDRecord.ReadTerrain(_r[1], header, group);
            //if (_r[2] != null) using (_r[2]) DYNARecord.ReadNonfixed(_r[2], header, group);
        }
    }
}
