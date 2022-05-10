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

using System.Windows;
using System.Windows.Data;

namespace NutaDev.CsLib.Gui.Framework.WPF.Views.Bindings
{
    /// <summary>
    /// This class provides helper methods for <see cref="BindingBase"/> and derived types.
    /// </summary>
    public static class BindingsHelper
    {
        /// <summary>
        /// Creates binding.
        /// </summary>
        /// <param name="source">The binding source.</param>
        /// <param name="path">The binding path.</param>
        /// <param name="converter">The value converter.</param>
        /// <param name="converterParameter">The converter parameter.</param>
        /// <returns>The binding.</returns>
        public static Binding CreateBinding(object source, string path, IValueConverter converter, object converterParameter)
        {
            Binding binding = new Binding(path);
            binding.Source = source;
            binding.Converter = converter;
            binding.ConverterParameter = converterParameter;
            return binding;
        }

        /// <summary>
        /// Creates binding.
        /// </summary>
        /// <param name="dp">The dependency property. This parameter is nto used.</param>
        /// <param name="source">The binding source.</param>
        /// <param name="path">The binding path.</param>
        /// <param name="converter">The value converter.</param>
        /// <param name="converterParameter">The converter parameter.</param>
        /// <returns>The binding.</returns>
        public static Binding CreateBinding(DependencyProperty dp, object source, string path, IValueConverter converter, object converterParameter)
        {
            Binding binding = new Binding(path);
            binding.Source = source;
            binding.Converter = converter;
            binding.ConverterParameter = converterParameter;
            return binding;
        }

        /// <summary>
        /// Creates binding.
        /// </summary>
        /// <param name="target">Target of binding creation.</param>
        /// <param name="dp">Bound dependency property.</param>
        /// <param name="source">Value's source.</param>
        /// <param name="path">Path to value.</param>
        public static void CreateBinding(this FrameworkElement target, DependencyProperty dp, object source, string path)
        {
            Binding binding = new Binding(path);
            binding.Source = source;
            target.SetBinding(dp, binding);
        }

        /// <summary>
        /// Creates binding.
        /// </summary>
        /// <param name="target">Target of binding creation.</param>
        /// <param name="dp">Bound dependency property.</param>
        /// <param name="source">Value's source.</param>
        /// <param name="path">Path to value.</param>
        /// <param name="converter"></param>
        /// <param name="converterParameter"></param>
        public static void CreateBinding(this FrameworkElement target, DependencyProperty dp, object source, string path, IValueConverter converter, object converterParameter)
        {
            Binding binding = new Binding(path);
            binding.Source = source;
            binding.Converter = converter;
            binding.ConverterParameter = converterParameter;
            target.SetBinding(dp, binding);
        }
    }
}
