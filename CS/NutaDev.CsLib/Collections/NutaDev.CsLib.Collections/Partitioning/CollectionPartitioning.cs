// The MIT License (MIT)
// 
// Copyright (c) 2022 tariel36
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using NutaDev.CsLib.Maintenance.Exceptions.Factories;
using NutaDev.CsLib.Resources.Text.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NutaDev.CsLib.Collections.Partitioning
{
    /// <summary>
    /// Provides various utility class to partition the collections.
    /// </summary>
    public static class CollectionPartitioning
    {
        /// <summary>
        /// Maximum collection size.
        /// </summary>
        public const int MaximumCollectionSize = 100;

        /// <summary>
        /// Partitions <paramref name="objects"/> into collections of maximum size <paramref name="partitionSize"/>.
        /// </summary>
        /// <typeparam name="TObject">Object type.</typeparam>
        /// <param name="objects">Collection to partition.</param>
        /// <param name="partitionSize">Maximum partition size.</param>
        /// <returns>Collection of partitioned collections.</returns>
        public static List<List<TObject>> GetPartitions<TObject>(ICollection<TObject> objects, int partitionSize)
        {
            if (partitionSize <= 0)
            {
                throw ExceptionFactory.InvalidOperationException(Text._0_MustBeGreaterThanZero, partitionSize);
            }

            List<List<TObject>> partitions = new List<List<TObject>>();

            if (objects != null)
            {
                for (int i = 0, iterations = GetIterationCount(partitionSize, objects.Count); i < iterations; ++i)
                {
                    int startIndex = i * partitionSize;

                    List<TObject> singlePart = GetRange(objects, startIndex, partitionSize).ToList();

                    if (singlePart.Count > 0)
                    {
                        partitions.Add(singlePart);
                    }
                }
            }

            return partitions;
        }

        /// <summary>
        /// Gets range of objects from <paramref name="objects"/>, starting at <paramref name="startIndex"/> and taking <paramref name="count"/>.
        /// </summary>
        /// <typeparam name="TObject">Object type.</typeparam>
        /// <param name="objects">Source collection.</param>
        /// <param name="startIndex">Start index.</param>
        /// <param name="count">Count to take.</param>
        /// <returns>Range of objects.</returns>
        public static TObject[] GetRange<TObject>(TObject[] objects, int startIndex, int count)
        {
            if (objects == null)
            {
                return null;
            }

            if (startIndex >= objects.Length)
            {
                throw ExceptionFactory.InvalidOperationException(Text._0_MustBeSmallerThanCollectionSize, startIndex);
            }

            int remainingCount = CalculateRemainingCount(objects.Length, startIndex, count);

            TObject[] result = new TObject[remainingCount];

            Array.Copy(objects, startIndex, result, 0, remainingCount);

            return result;
        }

        /// <summary>
        /// Gets range of objects from <paramref name="objects"/>, starting at <paramref name="startIndex"/> and taking <paramref name="count"/>.
        /// </summary>
        /// <typeparam name="TObject">Object type.</typeparam>
        /// <param name="objects">Source collection.</param>
        /// <param name="startIndex">Start index.</param>
        /// <param name="count">Count to take.</param>
        /// <returns>Range of objects.</returns>
        public static List<TObject> GetRange<TObject>(List<TObject> objects, int startIndex, int count)
        {
            if (objects == null)
            {
                return null;
            }

            if (startIndex >= objects.Count)
            {
                throw ExceptionFactory.InvalidOperationException(Text._0_MustBeSmallerThanCollectionSize, startIndex);
            }

            int remainingCount = CalculateRemainingCount(objects.Count, startIndex, count);

            return objects.GetRange(startIndex, remainingCount);
        }

        /// <summary>
        /// Gets range of objects from <paramref name="objects"/>, starting at <paramref name="startIndex"/> and taking <paramref name="count"/>.
        /// </summary>
        /// <typeparam name="TObject">Object type.</typeparam>
        /// <param name="objects">Source collection.</param>
        /// <param name="startIndex">Start index.</param>
        /// <param name="count">Count to take.</param>
        /// <returns>Range of objects.</returns>
        public static ICollection<TObject> GetRange<TObject>(ICollection<TObject> objects, int startIndex, int count)
        {
            if (objects == null)
            {
                return null;
            }

            if (startIndex >= objects.Count)
            {
                return new List<TObject>();
            }

            int remainingCount = CalculateRemainingCount(objects.Count, startIndex, count);

            return objects.Skip(startIndex).Take(remainingCount).ToList();
        }

        /// <summary>
        /// Calculates count of iteration to perform based on <see cref="MaximumCollectionSize"/>.
        /// </summary>
        /// <param name="collection">Source collection.</param>
        /// <returns>Iteration count.</returns>
        public static int GetIterationCount(ICollection collection)
        {
            return GetIterationCount(MaximumCollectionSize, collection);
        }

        /// <summary>
        /// Calculates count of iteration to perform based on <paramref name="partitionSize"/>.
        /// </summary>
        /// <param name="partitionSize">Maximum collection size.</param>
        /// <param name="collection">Source collection.</param>
        /// <returns>Iteration count.</returns>
        public static int GetIterationCount(int partitionSize, ICollection collection)
        {
            return GetIterationCount(partitionSize, collection.Count);
        }

        /// <summary>
        /// Calculates count of iteration to perform based on <paramref name="partitionSize"/>.
        /// </summary>
        /// <param name="partitionSize">Maximum collection size.</param>
        /// <param name="collectionSize"0>Collection size.</param>
        /// <returns>Iteration count.</returns>
        public static int GetIterationCount(int partitionSize, int collectionSize)
        {
            if (partitionSize <= 0)
            {
                throw ExceptionFactory.InvalidOperationException(Text._0_MustBeGreaterThanZero, partitionSize);
            }

            return (collectionSize / partitionSize) + (((collectionSize % partitionSize) != 0) ? 1 : 0);
        }

        /// <summary>
        /// Calculates remaining count to partition.
        /// </summary>
        /// <param name="collectionSize">Collection size.</param>
        /// <param name="startIndex">Start index.</param>
        /// <param name="count">Count to take.</param>
        /// <returns>Remaining count.</returns>
        private static int CalculateRemainingCount(int collectionSize, int startIndex, int count)
        {
            int remainingCount = count;

            if (startIndex + remainingCount >= collectionSize)
            {
                remainingCount = collectionSize - startIndex;
            }

            return remainingCount;
        }
    }
}
