using Game.Core.Netstream;
using System;
using System.IO;

namespace Game.Estate.UltimaIX.FilePack
{
    public partial class DatFile : RecordGroup, IDisposable
    {
        public DatFile(StreamSink streamSink, string filePath, UltimaIXGame game)
            : base(streamSink, filePath)
        {
            if (_streamSink is StreamSinkServer)
                return;
            if (_streamSink is StreamSinkClient)
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
        ~DatFile() => Close();

        public void Close() { }

        public void SinkDataContains(string path)
        {
            var bytes = _streamSink.GetDataContains(() =>
            {
                var info = new StreamSink.DataInfo();
                Read(info);
                return info.ToArray();
            });
            if (_streamSink is StreamSinkServer)
                return;
            // write data
            if (path != null)
                File.WriteAllBytes(Path.Combine(path, ".set"), bytes);
            //ReadGRUP(new StreamSink.DataInfo(bytes), path);
        }

        void Read(StreamSink.DataInfo info = null)
        {
            Clear();
            foreach (var file in Directory.GetFiles(Path.Combine(FilePath, "static"), "terrain.*"))
                AddHeader(new Header { Label = Path.GetExtension(file).Substring(1) }, info);
        }
    }
}