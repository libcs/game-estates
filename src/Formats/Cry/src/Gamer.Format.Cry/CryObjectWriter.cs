using System.IO;

namespace Gamer.Format.Cry
{
    public abstract class CryObjectWriter
    {
        // ARGS
        public DirectoryInfo DataDir = null;
        public const bool NoConflicts = false;
        public const bool TiffTextures = false;
        public const bool SkipShieldNodes = false;
        public const bool SkipProxyNodes = false;
        public const bool GroupMeshes = true;
        public const bool Smooth = true;

        public CryFile CryData { get; internal set; }

        public CryObjectWriter(CryFile obj) => CryData = obj;

        public abstract void Write(string outputDir = null, bool preservePath = true);

        protected string GetOutputFile(string extension, string outputDir = null, bool preservePath = true)
        {
            var outputFile = $"temp.{extension}";
            // Empty output directory means place alongside original models If you want relative path, use "."
            if (string.IsNullOrWhiteSpace(outputDir))
                outputFile = Path.Combine(new FileInfo(CryData.InputFile).DirectoryName, $"{Path.GetFileNameWithoutExtension(CryData.InputFile)}{(NoConflicts ? "_out" : string.Empty)}.{extension}");
            else
            {
                // If we have an output directory
                var preserveDir = preservePath ? Path.GetDirectoryName(CryData.InputFile) : string.Empty;
                // Remove drive letter if necessary
                if (!string.IsNullOrWhiteSpace(preserveDir) && !string.IsNullOrWhiteSpace(Path.GetPathRoot(preserveDir)))
                    preserveDir = preserveDir.Replace(Path.GetPathRoot(preserveDir), string.Empty);
                outputFile = Path.Combine(outputDir, preserveDir, Path.ChangeExtension(Path.GetFileNameWithoutExtension(CryData.InputFile), extension));
            }
            return outputFile;
        }
    }
}
