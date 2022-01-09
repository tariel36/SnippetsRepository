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
using System.Reflection;

namespace NutaDev.CsLib.Structures.Tuples
{
    /// <summary>
    /// <para>Base class for generic sets of mutable values.</para>
    /// <para>The <see cref="MutableTuple"/> works similar way to <see cref="Tuple"/> however the <see cref="MutableTuple"/> is mutable and the <see cref="Tuple"/> is not.</para>
    /// </summary>
    public class MutableTuple
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MutableTuple"/> class.
        /// </summary>
        /// <param name="size">Size of tuple.</param>
        protected MutableTuple(byte size)
        {
            Size = size;
        }

        /// <summary>
        /// Gets size of tuple.
        /// </summary>
        public byte Size { get; }

        /// <summary>
        /// Gets or sets tuple property by index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public object this[int index]
        {
            get { return GetValue(index); }
            set { SetValue(index, value); }
        }

        /// <summary>
        /// Returns tuple value by <paramref name="index"/>.
        /// </summary>
        /// <param name="index">Item's index.</param>
        /// <returns>Item's value.</returns>
        /// <exception cref="IndexOutOfRangeException">If specific property was not found.</exception>
        private object GetValue(int index)
        {
            PropertyInfo propertyInfo = GetType().GetProperty($"Item{index}");

            if (propertyInfo != null)
            {
                return propertyInfo.GetValue(this);
            }

            throw ExceptionFactory.Create<IndexOutOfRangeException>(Text.Index_0_MaxIndex_1, index, Size);
        }

        /// <summary>
        /// Sets property <paramref name="value"/> specified by <paramref name="index"/>.
        /// </summary>
        /// <param name="index">Item's index.</param>
        /// <param name="value">Value to set.</param>
        /// <exception cref="IndexOutOfRangeException">If specific property was not found.</exception>
        private void SetValue(int index, object value)
        {
            PropertyInfo propertyInfo = GetType().GetProperty($"Item{index}");

            if (propertyInfo == null)
            {
                throw ExceptionFactory.Create<IndexOutOfRangeException>(Text.Index_0_MaxIndex_1, index, Size);
            }

            propertyInfo.SetValue(this, value);
        }
    }

    /// <summary>
    /// Two element <see cref="MutableTuple"/>.
    /// </summary>
    /// <typeparam name="T1">Item1 type.</typeparam>
    /// <typeparam name="T2">Item2 type.</typeparam>
    public class MutableTuple<T1, T2>
        : MutableTuple
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MutableTuple{T1,T2}"/> class.
        /// </summary>
        public MutableTuple()
            : base(2)
        {
        }

        /// <summary>
        /// First item.
        /// </summary>
        public T1 Item1 { get; set; }

        /// <summary>
        /// Second item.
        /// </summary>
        public T2 Item2 { get; set; }
    }

    /// <summary>
    /// Three element <see cref="MutableTuple"/>.
    /// </summary>
    /// <typeparam name="T1">Item1 type.</typeparam>
    /// <typeparam name="T2">Item2 type.</typeparam>
    /// <typeparam name="T3">Item3 type.</typeparam>
    public class MutableTuple<T1, T2, T3>
        : MutableTuple
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MutableTuple{T1,T2,T3}"/> class.
        /// </summary>
        public MutableTuple()
            : base(3)
        {
        }

        /// <summary>
        /// First item.
        /// </summary>
        public T1 Item1 { get; set; }

        /// <summary>
        /// Second item.
        /// </summary>
        public T2 Item2 { get; set; }

        /// <summary>
        /// Third item.
        /// </summary>
        public T3 Item3 { get; set; }
    }

    /// <summary>
    /// Four element <see cref="MutableTuple"/>.
    /// </summary>
    /// <typeparam name="T1">Item1 type.</typeparam>
    /// <typeparam name="T2">Item2 type.</typeparam>
    /// <typeparam name="T3">Item3 type.</typeparam>
    /// <typeparam name="T4">Item4 type.</typeparam>
    public class MutableTuple<T1, T2, T3, T4>
        : MutableTuple
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MutableTuple{T1,T2,T3,T4}"/> class.
        /// </summary>
        public MutableTuple()
            : base(4)
        {
        }

        /// <summary>
        /// First item.
        /// </summary>
        public T1 Item1 { get; set; }

        /// <summary>
        /// Second item.
        /// </summary>
        public T2 Item2 { get; set; }

        /// <summary>
        /// Third item.
        /// </summary>
        public T3 Item3 { get; set; }

        /// <summary>
        /// Fourth item.
        /// </summary>
        public T4 Item4 { get; set; }
    }

    /// <summary>
    /// Five element <see cref="MutableTuple"/>.
    /// </summary>
    /// <typeparam name="T1">Item1 type.</typeparam>
    /// <typeparam name="T2">Item2 type.</typeparam>
    /// <typeparam name="T3">Item3 type.</typeparam>
    /// <typeparam name="T4">Item4 type.</typeparam>
    /// <typeparam name="T5">Item5 type.</typeparam>
    public class MutableTuple<T1, T2, T3, T4, T5>
        : MutableTuple
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MutableTuple{T1,T2,T3,T4,T5}"/> class.
        /// </summary>
        public MutableTuple()
            : base(5)
        {
        }

        /// <summary>
        /// First item.
        /// </summary>
        public T1 Item1 { get; set; }

        /// <summary>
        /// Second item.
        /// </summary>
        public T2 Item2 { get; set; }

        /// <summary>
        /// Third item.
        /// </summary>
        public T3 Item3 { get; set; }

        /// <summary>
        /// Fourth item.
        /// </summary>
        public T4 Item4 { get; set; }

        /// <summary>
        /// Fifth item.
        /// </summary>
        public T5 Item5 { get; set; }
    }

    /// <summary>
    /// Six element <see cref="MutableTuple"/>.
    /// </summary>
    /// <typeparam name="T1">Item1 type.</typeparam>
    /// <typeparam name="T2">Item2 type.</typeparam>
    /// <typeparam name="T3">Item3 type.</typeparam>
    /// <typeparam name="T4">Item4 type.</typeparam>
    /// <typeparam name="T5">Item5 type.</typeparam>
    /// <typeparam name="T6">Item6 type.</typeparam>
    public class MutableTuple<T1, T2, T3, T4, T5, T6>
        : MutableTuple
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MutableTuple{T1,T2,T3,T4,T5,T6}"/> class.
        /// </summary>
        public MutableTuple()
            : base(6)
        {
        }

        /// <summary>
        /// First item.
        /// </summary>
        public T1 Item1 { get; set; }

        /// <summary>
        /// Second item.
        /// </summary>
        public T2 Item2 { get; set; }

        /// <summary>
        /// Third item.
        /// </summary>
        public T3 Item3 { get; set; }

        /// <summary>
        /// Fourth item.
        /// </summary>
        public T4 Item4 { get; set; }

        /// <summary>
        /// Fifth item.
        /// </summary>
        public T5 Item5 { get; set; }

        /// <summary>
        /// Sixth item.
        /// </summary>
        public T6 Item6 { get; set; }
    }

    /// <summary>
    /// Seven element <see cref="MutableTuple"/>.
    /// </summary>
    /// <typeparam name="T1">Item1 type.</typeparam>
    /// <typeparam name="T2">Item2 type.</typeparam>
    /// <typeparam name="T3">Item3 type.</typeparam>
    /// <typeparam name="T4">Item4 type.</typeparam>
    /// <typeparam name="T5">Item5 type.</typeparam>
    /// <typeparam name="T6">Item6 type.</typeparam>
    /// <typeparam name="T7">Item7 type.</typeparam>
    public class MutableTuple<T1, T2, T3, T4, T5, T6, T7>
        : MutableTuple
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MutableTuple{T1,T2,T3,T4,T5,T6,T7}"/> class.
        /// </summary>
        public MutableTuple()
            : base(7)
        {
        }

        /// <summary>
        /// First item.
        /// </summary>
        public T1 Item1 { get; set; }

        /// <summary>
        /// Second item.
        /// </summary>
        public T2 Item2 { get; set; }

        /// <summary>
        /// Third item.
        /// </summary>
        public T3 Item3 { get; set; }

        /// <summary>
        /// Fourth item.
        /// </summary>
        public T4 Item4 { get; set; }

        /// <summary>
        /// Fifth item.
        /// </summary>
        public T5 Item5 { get; set; }

        /// <summary>
        /// Sixth item.
        /// </summary>
        public T6 Item6 { get; set; }

        /// <summary>
        /// Eighth item.
        /// </summary>
        public T7 Item7 { get; set; }
    }

    /// <summary>
    /// Eight element <see cref="MutableTuple"/>.
    /// </summary>
    /// <typeparam name="T1">Item1 type.</typeparam>
    /// <typeparam name="T2">Item2 type.</typeparam>
    /// <typeparam name="T3">Item3 type.</typeparam>
    /// <typeparam name="T4">Item4 type.</typeparam>
    /// <typeparam name="T5">Item5 type.</typeparam>
    /// <typeparam name="T6">Item6 type.</typeparam>
    /// <typeparam name="T7">Item7 type.</typeparam>
    /// <typeparam name="T8">Item8 type.</typeparam>
    public class MutableTuple<T1, T2, T3, T4, T5, T6, T7, T8>
        : MutableTuple
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MutableTuple{T1,T2,T3,T4,T5,T6,T7,T8}"/> class.
        /// </summary>
        public MutableTuple()
            : base(8)
        {
        }

        /// <summary>
        /// First item.
        /// </summary>
        public T1 Item1 { get; set; }

        /// <summary>
        /// Second item.
        /// </summary>
        public T2 Item2 { get; set; }

        /// <summary>
        /// Third item.
        /// </summary>
        public T3 Item3 { get; set; }

        /// <summary>
        /// Fourth item.
        /// </summary>
        public T4 Item4 { get; set; }

        /// <summary>
        /// Fifth item.
        /// </summary>
        public T5 Item5 { get; set; }

        /// <summary>
        /// Sixth item.
        /// </summary>
        public T6 Item6 { get; set; }

        /// <summary>
        /// Eighth item.
        /// </summary>
        public T7 Item7 { get; set; }

        /// <summary>
        /// Seventh item.
        /// </summary>
        public T8 Item8 { get; set; }
    }

    /// <summary>
    /// Nine element <see cref="MutableTuple"/>.
    /// </summary>
    /// <typeparam name="T1">Item1 type.</typeparam>
    /// <typeparam name="T2">Item2 type.</typeparam>
    /// <typeparam name="T3">Item3 type.</typeparam>
    /// <typeparam name="T4">Item4 type.</typeparam>
    /// <typeparam name="T5">Item5 type.</typeparam>
    /// <typeparam name="T6">Item6 type.</typeparam>
    /// <typeparam name="T7">Item7 type.</typeparam>
    /// <typeparam name="T8">Item8 type.</typeparam>
    /// <typeparam name="T9">Item9 type.</typeparam>
    public class MutableTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>
        : MutableTuple
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MutableTuple{T1,T2,T3,T4,T5,T6,T7,T8,T9}"/> class.
        /// </summary>
        public MutableTuple()
            : base(9)
        {
        }

        /// <summary>
        /// First item.
        /// </summary>
        public T1 Item1 { get; set; }

        /// <summary>
        /// Second item.
        /// </summary>
        public T2 Item2 { get; set; }

        /// <summary>
        /// Third item.
        /// </summary>
        public T3 Item3 { get; set; }

        /// <summary>
        /// Fourth item.
        /// </summary>
        public T4 Item4 { get; set; }

        /// <summary>
        /// Fifth item.
        /// </summary>
        public T5 Item5 { get; set; }

        /// <summary>
        /// Sixth item.
        /// </summary>
        public T6 Item6 { get; set; }

        /// <summary>
        /// Eighth item.
        /// </summary>
        public T7 Item7 { get; set; }

        /// <summary>
        /// Seventh item.
        /// </summary>
        public T8 Item8 { get; set; }

        /// <summary>
        /// Ninth item.
        /// </summary>
        public T9 Item9 { get; set; }
    }

    /// <summary>
    /// Ten element <see cref="MutableTuple"/>.
    /// </summary>
    /// <typeparam name="T1">Item1 type.</typeparam>
    /// <typeparam name="T2">Item2 type.</typeparam>
    /// <typeparam name="T3">Item3 type.</typeparam>
    /// <typeparam name="T4">Item4 type.</typeparam>
    /// <typeparam name="T5">Item5 type.</typeparam>
    /// <typeparam name="T6">Item6 type.</typeparam>
    /// <typeparam name="T7">Item7 type.</typeparam>
    /// <typeparam name="T8">Item8 type.</typeparam>
    /// <typeparam name="T9">Item9 type.</typeparam>
    /// <typeparam name="T10">Item10 type.</typeparam>
    public class MutableTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
        : MutableTuple
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MutableTuple{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10}"/> class.
        /// </summary>
        public MutableTuple()
            : base(10)
        {
        }

        /// <summary>
        /// First item.
        /// </summary>
        public T1 Item1 { get; set; }

        /// <summary>
        /// Second item.
        /// </summary>
        public T2 Item2 { get; set; }

        /// <summary>
        /// Third item.
        /// </summary>
        public T3 Item3 { get; set; }

        /// <summary>
        /// Fourth item.
        /// </summary>
        public T4 Item4 { get; set; }

        /// <summary>
        /// Fifth item.
        /// </summary>
        public T5 Item5 { get; set; }

        /// <summary>
        /// Sixth item.
        /// </summary>
        public T6 Item6 { get; set; }

        /// <summary>
        /// Eighth item.
        /// </summary>
        public T7 Item7 { get; set; }

        /// <summary>
        /// Seventh item.
        /// </summary>
        public T8 Item8 { get; set; }

        /// <summary>
        /// Ninth item.
        /// </summary>
        public T9 Item9 { get; set; }

        /// <summary>
        /// Tenth item.
        /// </summary>
        public T10 Item10 { get; set; }
    }
}
