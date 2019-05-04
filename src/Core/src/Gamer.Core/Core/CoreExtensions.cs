using UnityEngine;

namespace Shared.Core
{
    public static class Extensions
    {
        /// <summary>
        /// Quickly checks if an ASCII encoded string is equal to a C# string.
        /// </summary>
        public static bool EqualsASCIIBytes(this string source, byte[] ASCIIBytes)
        {
            if (ASCIIBytes.Length != source.Length)
                return false;
            for (var i = 0; i < ASCIIBytes.Length; i++)
                if (ASCIIBytes[i] != source[i])
                    return false;
            return true;
        }

        public static Color B565ToColor(this ushort B565)
        {
            var R5 = (B565 >> 11) & 31;
            var G6 = (B565 >> 5) & 63;
            var B5 = B565 & 31;
            return new Color((float)R5 / 31, (float)G6 / 63, (float)B5 / 31, 1);
        }

        public static Color32 B565ToColor32(this ushort B565) => B565ToColor(B565);
    }
}