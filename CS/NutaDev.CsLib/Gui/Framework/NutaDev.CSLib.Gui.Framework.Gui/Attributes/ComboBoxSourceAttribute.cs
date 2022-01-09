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

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace NutaDev.CSLib.Gui.Framework.Gui.Attributes
{
    /// <summary>
    /// Indicates where to look for values with <see cref="PropertyToCollectionConverter"/>.
    /// </summary>
    public class ComboBoxSourceAttribute
        : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComboBoxSourceAttribute"/> class.
        /// </summary>
        /// <param name="sourceType">Type of object.</param>
        /// <param name="methodName">Property of object.</param>
        public ComboBoxSourceAttribute(Type sourceType, string methodName)
        {
            SourceType = sourceType;
            MethodName = methodName;

            MethodInfo methodInfo = sourceType.GetMethod(MethodName, BindingFlags.Public | BindingFlags.Static);

            ItemsSource = Expression.Lambda<Func<ICollection<string>>>(Expression.Call(methodInfo)).Compile();
        }

        /// <summary>
        /// Gets the type of object.
        /// </summary>
        public Type SourceType { get; }

        /// <summary>
        /// Gets name of method.
        /// </summary>
        public string MethodName { get; }

        /// <summary>
        /// Gets source of items.
        /// </summary>
        public Func<ICollection<string>> ItemsSource { get; }
    }
}
