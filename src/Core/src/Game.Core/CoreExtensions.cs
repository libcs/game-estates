﻿using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Core
{
    public static class CoreExtensions
    {
        public static TResult Result<TResult>(this Task<TResult> task) => (task ?? throw new ArgumentNullException(nameof(task))).GetAwaiter().GetResult();
        //public static TResult ResultPool<TResult>(this Task<TResult> task) => task.ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Ensures the ends with.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string EnsureEndsWith(this string source, string value) => source.EndsWith(value) ? source : source + value;

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

        public static uint FromBGR555(this ushort bgr555, bool addAlpha = true)
        {
            var a = addAlpha ? (byte)0xFF : (byte)0;
            var r = (byte)Math.Min(((bgr555 & 0x7C00) >> 10) * 8, byte.MaxValue);
            var g = (byte)Math.Min(((bgr555 & 0x03E0) >> 5) * 8, byte.MaxValue);
            var b = (byte)Math.Min(((bgr555 & 0x001F) >> 0) * 8, byte.MaxValue);
            var color =
                ((uint)(a << 24) & 0xFF000000) |
                ((uint)(r << 16) & 0x00FF0000) |
                ((uint)(g << 8) & 0x0000FF00) |
                ((uint)(b << 0) & 0x000000FF);
            return color;
        }

        public static bool ParseBoolean(this string value) => bool.TryParse(value, out var v) ? v : v;
        public static double ParseDouble(this string value) => double.TryParse(value, out var v) ? v : v;
        public static TimeSpan ParseTimeSpan(this string value) => TimeSpan.TryParse(value, out var v) ? v : v;
        public static int ParseInt32(this string value) => value.StartsWith("0x") ? int.TryParse(value.Substring(2), NumberStyles.HexNumber, null, out var v) ? v : v : int.TryParse(value, out v) ? v : v;

        public static class Random
        {
            static System.Random _random = new System.Random();
            public static int RandomValue(int low, int high) => _random.Next(low, high + 1);
        }

        public static T ReadT<T>(this BinaryReader source, int length) => UnsafeUtils.MarshalT<T>(source.ReadBytes(length), length);
        public static T[] ReadTArray<T>(this BinaryReader source, int length, int count) => UnsafeUtils.MarshalTArray<T>(source.ReadBytes(length), count);
    }
}