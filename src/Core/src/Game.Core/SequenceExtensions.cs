﻿using System;
using System.Collections;
using System.Collections.Generic;
using static Game.Core.CoreDebug;

namespace Game.Core
{
    /// <summary>
    /// A reference to a range of elements in a non-null array.
    /// </summary>
    public struct ArrayRange<T> : IEnumerable<T>
    {
        #region Enumerator

        /// <summary>
        /// An enumerator for the elements in an array range.
        /// </summary>
        public struct Enumerator : IEnumerator<T>
        {
            ArrayRange<T> _arrayRange;
            int _currentIndex;

            public Enumerator(ArrayRange<T> arrayRange)
            {
                _arrayRange = arrayRange;
                _currentIndex = arrayRange.offset - 1; // Enumerators start positioned before the first element.
            }

            public void Dispose()
            {
                _arrayRange = new ArrayRange<T>();
                _currentIndex = -1;
            }

            public T Current => _arrayRange.array[_currentIndex];
            object IEnumerator.Current => Current;

            public bool MoveNext() => ++_currentIndex < (_arrayRange.offset + _arrayRange.length);

            public void Reset() => _currentIndex = _arrayRange.offset - 1; // Enumerators start positioned before the first element.
        }

        #endregion

        /// <summary>
        /// Constructs an ArrayRange referring to an entire array.
        /// </summary>
        /// <param name="array">A non-null array.</param>
        public ArrayRange(T[] array)
        {
            Assert(array != null);
            this.array = array;
            offset = 0;
            length = array.Length;
        }

        /// <summary>
        /// Constructs an ArrayRange referring to a portion of an array.
        /// </summary>
        /// <param name="array">A non-null array.</param>
        /// <param name="offset">A nonnegative offset.</param>
        /// <param name="length">A nonnegative length.</param>
        public ArrayRange(T[] array, int offset, int length)
        {
            Assert(array != null && offset >= 0 && length >= 0 && (offset + length) <= array.Length);
            this.array = array;
            this.offset = offset;
            this.length = length;
        }

        public T[] array { get; }
        public int offset { get; }
        public int length { get; }

        public IEnumerator<T> GetEnumerator() => new Enumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public static class SequenceEx
    {
        public static T Last<T>(this T[] array)
        {
            Assert(array.Length > 0);
            return array[array.Length - 1];
        }

        public static T Last<T>(this List<T> list)
        {
            Assert(list.Count > 0);
            return list[list.Count - 1];
        }

        /// <summary>
        /// Calculates the minimum and maximum values of an array.
        /// </summary>
        public static void GetExtrema(this float[] array, out float min, out float max)
        {
            min = float.MaxValue;
            max = float.MinValue;
            foreach (var element in array)
            {
                min = Math.Min(min, element);
                max = Math.Max(max, element);
            }
        }

        /// <summary>
        /// Calculates the minimum and maximum values of a 2D array.
        /// </summary>
        public static void GetExtrema(this float[,] array, out float min, out float max)
        {
            min = float.MaxValue;
            max = float.MinValue;
            foreach (var element in array)
            {
                min = Math.Min(min, element);
                max = Math.Max(max, element);
            }
        }

        /// <summary>
        /// Calculates the minimum and maximum values of a 3D array.
        /// </summary>
        public static void GetExtrema(this float[,,] array, out float min, out float max)
        {
            min = float.MaxValue;
            max = float.MinValue;
            foreach (var element in array)
            {
                min = Math.Min(min, element);
                max = Math.Max(max, element);
            }
        }

        public static void Flip2DArrayVertically<T>(T[] arr, int rowCount, int columnCount) { Flip2DSubArrayVertically(arr, 0, rowCount, columnCount); }
        /// <summary>
        /// Flips a portion of a 2D array vertically.
        /// </summary>
        /// <param name="arr">A 2D array represented as a 1D row-major array.</param>
        /// <param name="startIndex">The 1D index of the top left element in the portion of the 2D array we want to flip.</param>
        /// <param name="rows">The number of rows in the sub-array.</param>
        /// <param name="bytesPerRow">The number of columns in the sub-array.</param>
        public static void Flip2DSubArrayVertically<T>(T[] arr, int startIndex, int rows, int bytesPerRow)
        {
            Assert(startIndex >= 0 && rows >= 0 && bytesPerRow >= 0 && (startIndex + (rows * bytesPerRow)) <= arr.Length);
            var tmpRow = new T[bytesPerRow];
            var lastRowIndex = rows - 1;
            for (var rowIndex = 0; rowIndex < (rows / 2); rowIndex++)
            {
                var otherRowIndex = lastRowIndex - rowIndex;
                var rowStartIndex = startIndex + (rowIndex * bytesPerRow);
                var otherRowStartIndex = startIndex + (otherRowIndex * bytesPerRow);
                Array.Copy(arr, otherRowStartIndex, tmpRow, 0, bytesPerRow); // other -> tmp
                Array.Copy(arr, rowStartIndex, arr, otherRowStartIndex, bytesPerRow); // row -> other
                Array.Copy(tmpRow, 0, arr, rowStartIndex, bytesPerRow); // tmp -> row
            }
        }
    }
}