namespace Gamer.Estate.Tes.FilePack
{
    public partial class BsaMultiProxy : BsaMultiFile
    {
        public BsaMultiProxy(string url) : base(null) { }

        public override void Close() { }

        public override bool ContainsFile(string filePath) => false;

        public override byte[] LoadFileData(string filePath) => null;
    }
}