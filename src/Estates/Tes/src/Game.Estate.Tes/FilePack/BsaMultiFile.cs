using Game.Core.Netstream;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Game.Estate.Tes.FilePack
{
    /// <summary>
    /// BsaMultiFile
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public partial class BsaMultiFile : IDisposable
    {
        readonly StreamSink _streamSink;
        public readonly List<BsaFile> Packs = new List<BsaFile>();

        /// <summary>
        /// Initializes a new instance of the <see cref="BsaMultiFile" /> class.
        /// </summary>
        /// <param name="streamSink">The stream sink.</param>
        /// <param name="filePaths">The file paths.</param>
        /// <exception cref="System.ArgumentNullException">filePaths</exception>
        /// <exception cref="ArgumentNullException">filePaths</exception>
        public BsaMultiFile(StreamSink streamSink, string[] filePaths)
        {
            _streamSink = streamSink;
            if (_streamSink is StreamSinkClient)
                return;
            var files = (filePaths ?? throw new ArgumentNullException(nameof(filePaths)))
                .Where(x => Path.GetExtension(x) == ".bsa" || Path.GetExtension(x) == ".ba2");
            Packs.AddRange(files.Select(x => new BsaFile(x)));
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() => Close();

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            if (Packs != null)
                foreach (var pack in Packs)
                    pack.Close();
        }

        /// <summary>
        /// Gets the contains set.
        /// </summary>
        /// <returns></returns>
        public HashSet<string> GetContainsSet() => _streamSink.GetContainsSet(() => Packs.Aggregate(new HashSet<string>(StringComparer.OrdinalIgnoreCase), (a, b) => { a.UnionWith(b.GetContainsSet()); return a; }));

        /// <summary>
        /// Determines whether the BSA archive contains a file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>
        ///   <c>true</c> if the specified file path contains file; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsFile(string filePath) => _streamSink.ContainsFile(filePath, () => Packs.Any(x => x.ContainsFile(filePath)));

        /// <summary>
        /// Loads an archived file's data.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        /// <exception cref="System.IO.FileNotFoundException">Could not find file \"{filePath}</exception>
        public Task<byte[]> LoadFileDataAsync(string filePath) => _streamSink.LoadFileDataAsync(filePath, () =>
            (Packs.FirstOrDefault(x => x.ContainsFile(filePath)) ?? throw new FileNotFoundException($"Could not find file \"{filePath}\" in a BSA file."))
            .LoadFileDataAsync(filePath));
    }
}