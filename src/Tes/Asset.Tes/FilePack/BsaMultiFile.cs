using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Asset.Tes.FilePack
{
    /// <summary>
    /// BsaMultiFile
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public partial class BsaMultiFile : IDisposable
    {
        public readonly List<BsaFile> Packs = new List<BsaFile>();
        readonly ILogger _log;

        /// <summary>
        /// Initializes a new instance of the <see cref="BsaMultiFile"/> class.
        /// </summary>
        /// <param name="filePaths">The file paths.</param>
        /// <exception cref="ArgumentNullException">filePaths</exception>
        public BsaMultiFile(IServiceProvider services, string[] filePaths)
        {
            if (filePaths == null)
                throw new ArgumentNullException(nameof(filePaths));
            _log = services.GetRequiredService<ILogger>();
            var files = filePaths.Where(x => Path.GetExtension(x) == ".bsa" || Path.GetExtension(x) == ".ba2");
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
            foreach (var pack in Packs)
                pack.Close();
        }

        /// <summary>
        /// Determines whether the BSA archive contains a file.
        /// </summary>
        public virtual bool ContainsFile(string filePath) => Packs.Any(x => x.ContainsFile(filePath));

        /// <summary>
        /// Loads an archived file's data.
        /// </summary>
        public virtual byte[] LoadFileData(string filePath)
        {
            var pack = Packs.FirstOrDefault(x => x.ContainsFile(filePath));
            if (pack == null)
                throw new FileNotFoundException($"Could not find file \"{filePath}\" in a BSA file.");
            return pack.LoadFileData(filePath);
        }
    }
}