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

using System.IO;
using System.Text;
using System.Windows;

namespace NutaDev.CsLib.Gui.Framework.WPF.Views.AttachedProperties.Controls.RichTextBox
{
    /// <summary>
    /// Attached property class that adds the RtfText property to <see cref="RichTextBox"/>.
    /// </summary>
    public class RichTextBoxAttachedProperties
    {
        /// <summary>
        /// Dependency property name.
        /// </summary>
        private const string RtfText = nameof(RtfText);

        /// <summary>
        /// The attached property.
        /// </summary>
        public static readonly DependencyProperty RtfTextProperty = DependencyProperty.RegisterAttached(RtfText,
            typeof(string),
            typeof(RichTextBoxAttachedProperties),
            new PropertyMetadata(OnRtfTextChanged));

        /// <summary>
        /// Gets the RtfText.
        /// </summary>
        /// <param name="obj">Source object.</param>
        /// <returns>Rtf text or string's default value.</returns>
        public static string GetRtfText(DependencyObject obj)
        {
            return (string)obj.GetValue(RtfTextProperty);
        }

        /// <summary>
        /// Sets the RtfText.
        /// </summary>
        /// <param name="obj">Target <see cref="RichTextBox"/>.</param>
        /// <param name="value">Text to set.</param>
        public static void SetRtfText(DependencyObject obj, string value)
        {
            obj.SetValue(RtfTextProperty, value);
        }

        /// <summary>
        /// Fires when RtfText attached property has been changed. Sets the RTF encoded text to <see cref="RichTextBox"/>.
        /// </summary>
        /// <param name="obj">Target <see cref="RichTextBox"/>.</param>
        /// <param name="args">Event arguments.</param>
        public static void OnRtfTextChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            System.Windows.Controls.RichTextBox rtb = obj as System.Windows.Controls.RichTextBox;

            if (rtb == null) { return; }

            string rtfStr = args.NewValue as string;

            if (rtfStr != null)
            {
                rtb.Document.Blocks.Clear();

                using (MemoryStream stream = new MemoryStream(Encoding.Default.GetBytes(rtfStr)))
                {
                    rtb.Selection.Load(stream, DataFormats.Rtf);
                }
            }
        }
    }
}
