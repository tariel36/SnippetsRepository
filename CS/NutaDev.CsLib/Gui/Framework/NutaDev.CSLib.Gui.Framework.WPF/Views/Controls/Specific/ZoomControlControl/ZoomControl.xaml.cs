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
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NutaDev.CsLib.Gui.Framework.WPF.Views.Controls.Specific.ZoomControlControl
{
    /// <summary>
    /// Interaction logic for ZoomControl.xaml
    /// </summary>
    public partial class ZoomControl
        : UserControl
    {
        /// <summary>
        /// Value used to calculate scale.
        /// </summary>
        private const double ScaleDivider = 100.0f;

        /// <summary>
        /// <see cref="Transformable"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TransformableProperty = DependencyProperty.Register(nameof(Transformable),
            typeof(FrameworkElement),
            typeof(ZoomControl),
            new PropertyMetadata(default(FrameworkElement), PropertyChangedCallback));

        /// <summary>
        /// <see cref="ResetText"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ResetTextProperty = DependencyProperty.Register(nameof(ResetText),
            typeof(string),
            typeof(ZoomControl),
            new PropertyMetadata(default(string), PropertyChangedCallback));

        /// <summary>
        /// <see cref="ZoomMinimum"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ZoomMinimumProperty = DependencyProperty.Register(nameof(ZoomMinimum),
            typeof(int),
            typeof(ZoomControl),
            new PropertyMetadata(default(int), PropertyChangedCallback));

        /// <summary>
        /// <see cref="ZoomMaximum"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ZoomMaximumProperty = DependencyProperty.Register(nameof(ZoomMaximum),
            typeof(int),
            typeof(ZoomControl),
            new PropertyMetadata(default(int), PropertyChangedCallback));

        /// <summary>
        /// <see cref="ZoomCurrent"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ZoomCurrentProperty = DependencyProperty.Register(nameof(ZoomCurrent),
            typeof(int),
            typeof(ZoomControl),
            new PropertyMetadata(default(int), PropertyChangedCallback));

        /// <summary>
        /// <see cref="ZoomInterval"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ZoomIntervalProperty = DependencyProperty.Register(nameof(ZoomInterval),
            typeof(int),
            typeof(ZoomControl),
            new PropertyMetadata(default(int), PropertyChangedCallback));

        /// <summary>
        /// <see cref="ScrollViewer"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ScrollViewerProperty = DependencyProperty.Register(nameof(ScrollViewer),
            typeof(ScrollViewer),
            typeof(ZoomControl),
            new PropertyMetadata(default(ScrollViewer), PropertyChangedCallback));

        /// <summary>
        /// <see cref="DefaultZoom"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DefaultZoomProperty = DependencyProperty.Register(nameof(DefaultZoom),
            typeof(int),
            typeof(ZoomControl),
            new PropertyMetadata(default(int), PropertyChangedCallback));

        /// <summary>
        /// <see cref="ScrollHorizontalKey"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ScrollHorizontalKeyProperty = DependencyProperty.Register(nameof(ScrollHorizontalKey),
            typeof(Key),
            typeof(ZoomControl),
            new PropertyMetadata(default(Key), PropertyChangedCallback));

        /// <summary>
        /// <see cref="ScrollVerticalKey"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ScrollVerticalKeyProperty = DependencyProperty.Register(nameof(ScrollVerticalKey),
            typeof(Key),
            typeof(ZoomControl),
            new PropertyMetadata(default(Key)));

        /// <summary>
        /// <see cref="ZoomKey"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ZoomKeyProperty = DependencyProperty.Register(nameof(ZoomKey),
            typeof(Key),
            typeof(ZoomControl),
            new PropertyMetadata(default(Key), PropertyChangedCallback));

        /// <summary>
        /// Initializes a new instance of the <see cref="ZoomControl"/> class.
        /// </summary>
        public ZoomControl()
        {
            InitializeComponent();

            SpriteCanvasScaleTransform = new ScaleTransform();
        }

        /// <summary>
        /// Gets or sets scroll viewer.
        /// </summary>
        public ScrollViewer ScrollViewer
        {
            get { return (ScrollViewer)GetValue(ScrollViewerProperty); }
            set { SetValue(ScrollViewerProperty, value); }
        }

        /// <summary>
        /// Gets or sets zoom interval.
        /// </summary>
        public int ZoomInterval
        {
            get { return (int)GetValue(ZoomIntervalProperty); }
            set { SetValue(ZoomIntervalProperty, value); }
        }

        /// <summary>
        /// Gets or sets current zoom.
        /// </summary>
        public int ZoomCurrent
        {
            get { return (int)GetValue(ZoomCurrentProperty); }
            set { SetValue(ZoomCurrentProperty, value); }
        }

        /// <summary>
        /// Gets or sets maximum zoom.
        /// </summary>
        public int ZoomMaximum
        {
            get { return (int)GetValue(ZoomMaximumProperty); }
            set { SetValue(ZoomMaximumProperty, value); }
        }

        /// <summary>
        /// Gets or set minimum zoom.
        /// </summary>
        public int ZoomMinimum
        {
            get { return (int)GetValue(ZoomMinimumProperty); }
            set { SetValue(ZoomMinimumProperty, value); }
        }

        /// <summary>
        /// Gets or sets reset button text.
        /// </summary>
        public string ResetText
        {
            get { return (string)GetValue(ResetTextProperty); }
            set { SetValue(ResetTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets transformed object.
        /// </summary>
        public FrameworkElement Transformable
        {
            get { return (FrameworkElement)GetValue(TransformableProperty); }
            set { SetValue(TransformableProperty, value); }
        }

        /// <summary>
        /// Gets or sets default zoom value.
        /// </summary>
        public int DefaultZoom
        {
            get { return (int)GetValue(DefaultZoomProperty); }
            set { SetValue(DefaultZoomProperty, value); }
        }

        /// <summary>
        /// Gets or sets zoom key.
        /// </summary>
        public Key ZoomKey
        {
            get { return (Key)GetValue(ZoomKeyProperty); }
            set { SetValue(ZoomKeyProperty, value); }
        }

        /// <summary>
        /// Gets or sets horizontal scroll key.
        /// </summary>
        public Key ScrollHorizontalKey
        {
            get { return (Key)GetValue(ScrollHorizontalKeyProperty); }
            set { SetValue(ScrollHorizontalKeyProperty, value); }
        }

        /// <summary>
        /// Gets or sets vertical scroll key.
        /// </summary>
        public Key ScrollVerticalKey
        {
            get { return (Key)GetValue(ScrollVerticalKeyProperty); }
            set { SetValue(ScrollVerticalKeyProperty, value); }
        }

        /// <summary>
        /// Gets scale transform.
        /// </summary>
        private ScaleTransform SpriteCanvasScaleTransform { get; }

        /// <summary>
        /// Handles property updates.
        /// </summary>
        /// <param name="dependencyObject">Event sender.</param>
        /// <param name="args">Event args.</param>
        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is ZoomControl ctrl && !string.IsNullOrWhiteSpace(args.Property?.Name))
            {
                switch (args.Property.Name)
                {
                    case nameof(Transformable):
                    {
                        if (ctrl.Transformable != null)
                        {
                            ctrl.Transformable.LayoutTransform = null;
                            ctrl.Transformable.MouseWheel -= ctrl.SpriteCanvas_OnMouseWheel;
                        }

                        ctrl.Reset();
                        ctrl.Transformable = args.NewValue as FrameworkElement;

                        if (ctrl.Transformable != null)
                        {
                            ctrl.Transformable.LayoutTransform = ctrl.SpriteCanvasScaleTransform;
                            ctrl.Transformable.MouseWheel += ctrl.SpriteCanvas_OnMouseWheel;
                        }

                        break;
                    }
                    case nameof(ZoomInterval):
                    {
                        ctrl.ZoomSlider.Interval = (int)args.NewValue;

                        break;
                    }
                    case nameof(ZoomCurrent):
                    {
                        ctrl.ZoomSlider.Value = (int)args.NewValue;

                        break;
                    }
                    case nameof(ZoomMinimum):
                    {
                        ctrl.ZoomSlider.Minimum = (int)args.NewValue;

                        break;
                    }
                    case nameof(ZoomMaximum):
                    {
                        ctrl.ZoomSlider.Maximum = (int)args.NewValue;
                        break;
                    }
                    case nameof(ResetText):
                    {
                        ctrl.ResetButton.Content = args.NewValue;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Handles mouse wheel event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void SpriteCanvas_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(ZoomKey) && ZoomSlider != null)
            {
                if (e.Delta > 0)
                {
                    ZoomSlider.Value += ZoomSlider.Interval;
                }
                else
                {
                    ZoomSlider.Value -= ZoomSlider.Interval;
                }

                e.Handled = true;
            }
            else if (Keyboard.IsKeyDown(ScrollHorizontalKey) && ScrollViewer != null)
            {
                if (e.Delta > 0)
                {
                    ScrollViewer.LineLeft();
                }
                else
                {
                    ScrollViewer.LineRight();
                }

                e.Handled = true;
            }
            else if (Keyboard.IsKeyDown(ScrollVerticalKey) && ScrollViewer != null)
            {
                if (e.Delta > 0)
                {
                    ScrollViewer.LineUp();
                }
                else
                {
                    ScrollViewer.LineDown();
                }

                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles zoom slider value changed.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void ZoomSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SpriteCanvasScaleTransform != null)
            {
                SpriteCanvasScaleTransform.ScaleX = ZoomSlider.Value / ScaleDivider;
                SpriteCanvasScaleTransform.ScaleY = ZoomSlider.Value / ScaleDivider;
            }
        }

        /// <summary>
        /// Handles reset button click.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void ResetButton_OnClick(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        /// <summary>
        /// Resets zoom.
        /// </summary>
        private void Reset()
        {
            if (ZoomSlider != null)
            {
                ZoomSlider.Value = DefaultZoom;
            }
        }
    }
}
