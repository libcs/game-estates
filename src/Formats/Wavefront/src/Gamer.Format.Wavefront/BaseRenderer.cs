using Gamer.Format.Cry;
using System.IO;

namespace Gamer.Format.Wavefront
{
    public abstract class BaseRenderer
    {
        // ARGS
        public DirectoryInfo DataDir = null;
        public const bool NoConflicts = false;
        public const bool TiffTextures = false;
        public const bool SkipShieldNodes = false;
        public const bool SkipProxyNodes = false;
        public const bool GroupMeshes = true;
        public const bool Smooth = true;

        public CryEngine CryData { get; internal set; }

        public BaseRenderer(CryEngine cryEngine) => CryData = cryEngine;

        public abstract void Render(string outputDir = null, bool preservePath = true);

        internal string GetOutputFile(string extension, string outputDir = null, bool preservePath = true)
        {
            var outputFile = $"temp.{extension}";
            // Empty output directory means place alongside original models If you want relative path, use "."
            if (string.IsNullOrWhiteSpace(outputDir))
                outputFile = Path.Combine(new FileInfo(CryData.InputFile).DirectoryName, $"{Path.GetFileNameWithoutExtension(CryData.InputFile)}{(NoConflicts ? "_out" : "")}.{extension}");
            else
            {
                // If we have an output directory
                var preserveDir = preservePath ? Path.GetDirectoryName(CryData.InputFile) : "";
                // Remove drive letter if necessary
                if (!string.IsNullOrWhiteSpace(preserveDir) && !string.IsNullOrWhiteSpace(Path.GetPathRoot(preserveDir)))
                    preserveDir = preserveDir.Replace(Path.GetPathRoot(preserveDir), "");
                outputFile = Path.Combine(outputDir, preserveDir, Path.ChangeExtension(Path.GetFileNameWithoutExtension(this.CryData.InputFile), extension));
            }
            return outputFile;
        }
    }
}
