using Game.Core.Netstream;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Game.Estate.UltimaIX.FilePack
{
    public partial class ResMultiFile : IDisposable
    {
        readonly StreamSink _streamSink;
        public readonly List<ResFile> Packs = new List<ResFile>();

        public ResMultiFile(StreamSink streamSink, string[] filePaths)
        {
            _streamSink = streamSink;
            if (_streamSink is StreamSinkClient)
                return;
            var files = (filePaths ?? throw new ArgumentNullException(nameof(filePaths)))
                .Where(x => Path.GetExtension(x) == ".flx" || x.Contains("Texture8.") || x.Contains("texture16."));
            Packs.AddRange(files.Select(x => new ResFile(x)));
        }

        public void Dispose() => Close();

        public void Close()
        {
            if (Packs != null)
                foreach (var pack in Packs)
                    pack.Close();
        }

        public HashSet<string> GetContainsSet() => _streamSink.GetContainsSet(() => Packs.Aggregate(new HashSet<string>(StringComparer.OrdinalIgnoreCase), (a, b) => { a.UnionWith(b.GetContainsSet()); return a; }));

        public bool ContainsFile(string filePath) => _streamSink.ContainsFile(filePath, () => Packs.Any(x => x.ContainsFile(filePath)));

        public Task<byte[]> LoadFileDataAsync(string filePath) => _streamSink.LoadFileDataAsync(filePath, () =>
            (Packs.FirstOrDefault(x => x.ContainsFile(filePath)) ?? throw new FileNotFoundException($"Could not find file \"{filePath}\" in a RES file."))
            .LoadFileDataAsync(filePath));
    }
}