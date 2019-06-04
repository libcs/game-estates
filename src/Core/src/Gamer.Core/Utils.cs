using System;
using System.Collections.Generic;
using System.Linq;
using static Gamer.Core.Debug;

namespace Gamer.Core
{
    /// <summary>
    /// Specifies an endianness
    /// </summary>
    public enum Endian
    {
        /// <summary>
        /// Little endian (i.e. DDCCBBAA)
        /// </summary>
        LittleEndian = 0,
        /// <summary>
        /// Big endian (i.e. AABBCCDD)
        /// </summary>
        BigEndian = 1
    }

    public class ByteArrayComparer : IEqualityComparer<byte[]>
    {
        public static ByteArrayComparer Default = new ByteArrayComparer();
        public bool Equals(byte[] left, byte[] right) => left == null || right == null ? left == right : left.SequenceEqual(right);
        public int GetHashCode(byte[] key) => (key ?? throw new ArgumentNullException(nameof(key))).Sum(b => b);
    }

    public static class Utils
    {
        public static string ToB64String(byte[] source)
        {
            var v = Convert.ToBase64String(source);
            var i = v.Length - 1; for (; i >= 0 && (v[i] == '=' || v[i] == 'A'); i--) ;
            return v.Substring(0, i + 1).Replace("/", "_").Replace("+", "-");
        }

        public static byte[] FromB64String(string source)
        {
            throw new NotImplementedException();
        }

        public static void Swap<T>(ref T a, ref T b)
        {
            var tmp = a;
            a = b;
            b = tmp;
        }

        /// <summary>
        /// Checks if a bit string (an unsigned integer) contains a collection of bit flags.
        /// </summary>
        public static bool ContainsBitFlags(int bits, params int[] bitFlags)
        {
            uint allBitFlags = 0;
            foreach (var bitFlag in bitFlags)
                allBitFlags |= (uint)bitFlag;
            return ((uint)bits & allBitFlags) == allBitFlags;
        }

        /// <summary>
        /// Extracts a range of bits from a byte array.
        /// </summary>
        /// <param name="bitOffset">An offset in bits from the most significant bit (byte 0, bit 0) of the byte array.</param>
        /// <param name="bitCount">The number of bits to extract. Cannot exceed 64.</param>
        /// <param name="bytes">A big-endian byte array.</param>
        /// <returns>A ulong containing the right-shifted extracted bits.</returns>
        public static ulong GetBits(uint bitOffset, uint bitCount, byte[] bytes)
        {
            Assert(bitCount <= 64 && (bitOffset + bitCount) <= (8 * bytes.Length));
            var bits = 0UL;
            var remainingBitCount = bitCount;
            var byteIndex = bitOffset / 8;
            var bitIndex = bitOffset - (8 * byteIndex);
            while (remainingBitCount > 0)
            {
                // Read bits from the byte array.
                var numBitsLeftInByte = 8 - bitIndex;
                var numBitsReadNow = Math.Min(remainingBitCount, numBitsLeftInByte);
                var unmaskedBits = (uint)bytes[byteIndex] >> (int)(8 - (bitIndex + numBitsReadNow));
                var bitMask = 0xFFu >> (int)(8 - numBitsReadNow);
                var bitsReadNow = unmaskedBits & bitMask;

                // Store the bits we read.
                bits <<= (int)numBitsReadNow;
                bits |= bitsReadNow;

                // Prepare for the next iteration.
                bitIndex += numBitsReadNow;

                if (bitIndex == 8)
                {
                    byteIndex++;
                    bitIndex = 0;
                }

                remainingBitCount -= numBitsReadNow;
            }
            return bits;
        }

        /// <summary>
        /// Transforms x from an element of [min0, max0] to an element of [min1, max1].
        /// </summary>
        public static float ChangeRange(float x, float min0, float max0, float min1, float max1)
        {
            Assert(min0 <= max0 && min1 <= max1 && x >= min0 && x <= max0);
            var range0 = max0 - min0;
            var range1 = max1 - min1;
            var xPct = range0 != 0 ? (x - min0) / range0 : 0;
            return min1 + (xPct * range1);
        }
    }
}