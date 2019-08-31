using Game.Core;
using System.IO;

namespace Game.Estate.Ultima.Resources
{
    public class RadarColorData
    {
        public static uint[] Colors = new uint[0x20000];

        const int multiplier = 0xFF / 0x1F;

        static RadarColorData()
        {
            using (var index = new FileStream(UltimaFileManager.GetFilePath("Radarcol.mul"), FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var r = new BinaryReader(index);
                // Prior to 7.0.7.1, all clients have 0x10000 colors. Newer clients have fewer colors.
                var colorCount = (int)index.Length / 2;
                for (var i = 0; i < colorCount; i++)
                {
                    var c = (uint)r.ReadUInt16();
                    Colors[i] = 0xFF000000 | (
                            ((((c >> 10) & 0x1F) * multiplier)) |
                            ((((c >> 5) & 0x1F) * multiplier) << 8) |
                            (((c & 0x1F) * multiplier) << 16)
                            );
                }
                // fill the remainder of the color table with non-transparent magenta.
                for (var i = colorCount; i < Colors.Length; i++)
                    Colors[i] = 0xFFFF00FF;
                Metrics.ReportDataRead((int)r.BaseStream.Position);
            }
        }
    }
}