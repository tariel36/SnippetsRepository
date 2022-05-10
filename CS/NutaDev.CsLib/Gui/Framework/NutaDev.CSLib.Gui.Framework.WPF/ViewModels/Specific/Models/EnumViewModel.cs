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

using NutaDev.CsLib.Core.Interfaces;
using NutaDev.CsLib.Gui.Framework.WPF.ViewModels.Abstract.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using NutaDev.CsLib.Collections.Extensions;

namespace NutaDev.CsLib.Gui.Framework.WPF.ViewModels.Specific.Models
{
    /// <summary>
    /// View model for enum values.
    /// </summary>
    [DebuggerDisplay("{Value}")]
    public class EnumViewModel
        : DataViewModel
        , ICloneable<EnumViewModel>
        , IDisplayString
    {
        /// <summary>
        /// Backing field for <see cref="Value"/>.
        /// </summary>
        private int _value;

        /// <summary>
        /// Backing field for <see cref="TextMap"/>.
        /// </summary>
        private Dictionary<int, string> _textMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumViewModel"/> class.
        /// </summary>
        /// <param name="type">Enum type.</param>
        public EnumViewModel(Type type)
            : this(type, 0)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumViewModel"/> class.
        /// </summary>
        /// <param name="type">Enum type.</param>
        /// <param name="value">Enum value.</param>
        public EnumViewModel(Type type, int value)
            : this(type, value, new Dictionary<int, string>())
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumViewModel"/> class.
        /// </summary>
        /// <param name="type">Enum type.</param>
        /// <param name="value">Enum value.</param>
        public EnumViewModel(Type type, Enum value)
            : this(type, (int)(object)value, new Dictionary<int, string>())
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumViewModel"/> class.
        /// </summary>
        /// <param name="type">Enum type.</param>
        /// <param name="value">Enum value.</param>
        /// <param name="textMap">Translation dictionary.</param>
        public EnumViewModel(Type type, int value, Dictionary<int, string> textMap)
        {
            TextMap = new Dictionary<int, string>(textMap);
            Type = type;
            Value = value;
        }

        /// <summary>
        /// Gets text of current value.
        /// </summary>
        public string DisplayString { get { return Text; } }

        /// <summary>
        /// Gets enum type.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Gets text of current value.
        /// </summary>
        public string Text
        {
            get { return TextMap != null && TextMap.ContainsKey(Value) ? TextMap[Value] : (Type == null ? Value.ToString() : Enum.Parse(Type, Value.ToString()).ToString()); }
        }

        /// <summary>
        /// Gets or sets current value.
        /// </summary>
        public int Value
        {
            get { return _value; }
            set
            {
                Set(ref _value, value);
            }
        }

        /// <summary>
        /// Gets or sets translations dictionary.
        /// </summary>
        public Dictionary<int, string> TextMap
        {
            get { return _textMap; }
            set
            {
                Set(ref _textMap, value);
            }
        }

        /// <summary>
        /// Creates instance of the <see cref="EnumViewModel"/> class.
        /// </summary>
        /// <typeparam name="T">Enum type.</typeparam>
        /// <param name="value">Enum value.</param>
        /// <returns>Instance of the <see cref="EnumViewModel"/> class.</returns>
        public static EnumViewModel Create<T>(T value)
        {
            return Create(value, new Dictionary<int, string>());
        }

        /// <summary>
        /// Creates instance of the <see cref="EnumViewModel"/> class.
        /// </summary>
        /// <typeparam name="T">Enum type.</typeparam>
        /// <param name="value">Enum value.</param>
        /// <param name="textMap">Dictionary of translations.</param>
        /// <returns>Instance of the <see cref="EnumViewModel"/> class.</returns>
        public static EnumViewModel Create<T>(T value, Dictionary<int, string> textMap)
        {
            return new EnumViewModel(typeof(T), (int)(object)value, textMap);
        }

        /// <summary>
        /// Returns <see cref="ObservableCollection{T}"/> of all enum values.
        /// </summary>
        /// <typeparam name="T">Enum type.</typeparam>
        /// <param name="textMap">Dictionary of translations.</param>
        /// <param name="lowerBound">Indicates lowest value of an enum.</param>
        /// <param name="upperBound">Indicates highest value of an enum.</param>
        /// <param name="includeLowerBound">Determines whether lowest enum value should be included.</param>
        /// <param name="includeUpperBound">Determines whether highes enum value should be included.</param>
        /// <returns>Dictionary of enum values.</returns>
        public static ObservableCollection<EnumViewModel> CreateCollection<T>(Dictionary<int, string> textMap, int lowerBound = 0, int upperBound = int.MaxValue, bool includeLowerBound = true, bool includeUpperBound = false)
        {
            return QueryForModels<T>(textMap, lowerBound, upperBound, includeLowerBound, includeUpperBound)
                .ToObservableCollection();
        }

        /// <summary>
        /// Returns <see cref="Dictionary{TKey,TValue}"/> of all enum values.
        /// </summary>
        /// <typeparam name="T">Enum type.</typeparam>
        /// <param name="textMap">Dictionary of translations.</param>
        /// <param name="lowerBound">Indicates lowest value of an enum.</param>
        /// <param name="upperBound">Indicates highest value of an enum.</param>
        /// <param name="includeLowerBound">Determines whether lowest enum value should be included.</param>
        /// <param name="includeUpperBound">Determines whether highes enum value should be included.</param>
        /// <returns>Dictionary of enum values.</returns>
        public static Dictionary<T, EnumViewModel> CreateDictionary<T>(Dictionary<int, string> textMap, int lowerBound = 0, int upperBound = int.MaxValue, bool includeLowerBound = true, bool includeUpperBound = false)
        {
            return QueryForModels<T>(textMap, lowerBound, upperBound, includeLowerBound, includeUpperBound)
                .GroupBy(x => x.EnumValue<T>())
                .ToDictionary(k => k.Key, v => v.First());
        }

        /// <summary>
        /// Returns collection of all enum values.
        /// </summary>
        /// <typeparam name="T">Enum type.</typeparam>
        /// <param name="textMap">Dictionary of translations.</param>
        /// <param name="lowerBound">Indicates lowest value of an enum.</param>
        /// <param name="upperBound">Indicates highest value of an enum.</param>
        /// <param name="includeLowerBound">Determines whether lowest enum value should be included.</param>
        /// <param name="includeUpperBound">Determines whether highes enum value should be included.</param>
        /// <returns>Collection of enum values.</returns>
        public static ICollection<EnumViewModel> QueryForModels<T>(Dictionary<int, string> textMap, int lowerBound = 0, int upperBound = int.MaxValue, bool includeLowerBound = true, bool includeUpperBound = false)
        {
            Type type = typeof(T);

            return Enum.GetValues(type)
                .Cast<int>()
                .Where(x => (x == lowerBound && includeLowerBound) || (x == upperBound && includeUpperBound) || (x > lowerBound && x < upperBound))
                .Select(x => Create((T)(object)x, textMap))
                .ToList();
        }

        /// <summary>
        /// Returns enum value of <see cref="Value"/>.
        /// </summary>
        /// <typeparam name="T">Enum type.</typeparam>
        /// <returns>Enum value of <see cref="Value"/>.</returns>
        public T EnumValue<T>()
        {
            return (T)(object)Value;
        }

        /// <summary>
        /// Creates clone of current instnace.
        /// </summary>
        /// <returns>Clone of current instance.</returns>
        public EnumViewModel Clone()
        {
            return new EnumViewModel(Type, Value, TextMap);
        }

        /// <summary>
        /// Checks equality of current instance and <paramref name="obj"/>. If <paramref name="obj"/> is <see cref="EnumViewModel"/> then <see cref="Value"/> properties are compared.
        /// </summary>
        /// <param name="obj">Other object.</param>
        /// <returns>True if <paramref name="obj"/> is equal to curent instance.</returns>
        public override bool Equals(object obj)
        {
            if (obj is EnumViewModel other)
            {
                return Value.Equals(other.Value);
            }

            return base.Equals(obj);
        }

        /// <summary>
        /// Returns <see cref="Value"/> hash code.
        /// </summary>
        /// <returns>Hash code of <see cref="Value"/>.</returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Returns <see cref="Value"/> string.
        /// </summary>
        /// <returns>String value of <see cref="Value"/>.</returns>
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
