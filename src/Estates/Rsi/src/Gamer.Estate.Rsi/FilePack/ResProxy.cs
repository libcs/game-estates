namespace Gamer.Estate.Rsi.FilePack
{
    public partial class ResProxy : ResFile
    {

        public ResProxy() : base(null) { }

        public override void Close() { }

        /// <summary>
        /// Determines whether the BSA archive contains a file.
        /// </summary>
        public override bool ContainsFile(string filePath) => false;

        /// <summary>
        /// Loads an archived file's data.
        /// </summary>
        public override byte[] LoadFileData(string filePath) => null;
    }
}