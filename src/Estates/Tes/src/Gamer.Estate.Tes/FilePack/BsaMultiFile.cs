using Gamer.Proxy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Gamer.Estate.Tes.FilePack
{
    /// <summary>
    /// BsaMultiFile
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public partial class BsaMultiFile : IDisposable
    {
        readonly ProxySink _proxySink;
        public readonly List<BsaFile> Packs;

        /// <summary>
        /// Initializes a new instance of the <see cref="BsaMultiFile" /> class.
        /// </summary>
        /// <param name="proxySink">The proxy sink.</param>
        /// <param name="filePaths">The file paths.</param>
        /// <exception cref="System.ArgumentNullException">filePaths</exception>
        /// <exception cref="ArgumentNullException">filePaths</exception>
        public BsaMultiFile(ProxySink proxySink, string[] filePaths)
        {
            _proxySink = proxySink;
            if (proxySink is ProxySinkClient)
                return;
            var files = (filePaths ?? throw new ArgumentNullException(nameof(filePaths))).Where(x => Path.GetExtension(x) == ".bsa" || Path.GetExtension(x) == ".ba2");
            Packs = new List<BsaFile>();
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
        /// Determines whether the BSA archive contains a file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>
        ///   <c>true</c> if the specified file path contains file; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsFile(string filePath) => _proxySink.ContainsFile(filePath, () => Packs.Any(x => x.ContainsFile(filePath)));

        /// <summary>
        /// Loads an archived file's data.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        /// <exception cref="System.IO.FileNotFoundException">Could not find file \"{filePath}</exception>
        public Task<byte[]> LoadFileDataAsync(string filePath) => _proxySink.LoadFileDataAsync(filePath, () =>
            (Packs.FirstOrDefault(x => x.ContainsFile(filePath)) ?? throw new FileNotFoundException($"Could not find file \"{filePath}\" in a BSA file."))
            .LoadFileDataAsync(filePath));
    }
}