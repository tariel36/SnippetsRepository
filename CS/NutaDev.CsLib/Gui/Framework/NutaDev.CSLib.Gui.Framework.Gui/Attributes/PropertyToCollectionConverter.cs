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
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace NutaDev.CSLib.Gui.Framework.Gui.Attributes
{
    /// <summary>
    /// Converts collection specified by property name into items source for property grid.
    /// </summary>
    public class PropertyToCollectionConverter
        : StringConverter
    {
        /// <summary>
        /// Default collection.
        /// </summary>
        private static readonly ICollection<string> DefaultCollection = new List<string>();

        /// <summary>
        /// Items source provider.
        /// </summary>
        private Func<ICollection<string>> ItemsSource { get; set; }

        /// <summary>
        /// Checks if should show a combobox.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Always true.</returns>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// Limits to list.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Always true.</returns>
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// Provides values.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Collection of values.</returns>
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return TryGetItems(context?.Instance, context?.PropertyDescriptor?.Name);
        }

        /// <summary>
        /// Tries to get items.
        /// </summary>
        /// <param name="instance">Object instance.</param>
        /// <param name="propertyName">Name of property.</param>
        /// <returns>Collection of values.</returns>
        private StandardValuesCollection TryGetItems(object instance, string propertyName)
        {
            if (ItemsSource == null)
            {
                if (instance != null && !string.IsNullOrWhiteSpace(propertyName))
                {
                    Type type = instance.GetType();
                    PropertyInfo pInfo = type.GetProperty(propertyName);

                    ItemsSource = pInfo?.GetCustomAttribute<ComboBoxSourceAttribute>()?.ItemsSource;
                }
            }

            ICollection<string> items;

            if (ItemsSource == null)
            {
                items = DefaultCollection;
            }
            else
            {
                items = ItemsSource() ?? DefaultCollection;
            }

            return new StandardValuesCollection(items.ToList());
        }
    }
}
