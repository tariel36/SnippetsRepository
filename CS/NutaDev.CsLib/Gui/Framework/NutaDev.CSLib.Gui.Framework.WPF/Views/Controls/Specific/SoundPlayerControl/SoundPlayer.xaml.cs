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

using NutaDev.CsLib.Maintenance.Exceptions.Delegates;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace NutaDev.CsLib.Gui.Framework.WPF.Views.Controls.Specific.SoundPlayerControl
{
    /// <summary>
    /// Interaction logic for SoundPlayer.xaml
    /// </summary>
    public partial class SoundPlayer
        : UserControl
    {
        /// <summary>
        /// Near modifier dependency property.
        /// </summary>
        public static readonly DependencyProperty NearModifierProperty = DependencyProperty.Register(nameof(NearModifier),
            typeof(int),
            typeof(SoundPlayer),
            new PropertyMetadata(default(int), PropertyChangedCallback));

        /// <summary>
        /// Far modifier dependency property.
        /// </summary>
        public static readonly DependencyProperty FarModifierProperty = DependencyProperty.Register(nameof(FarModifier),
            typeof(int),
            typeof(SoundPlayer),
            new PropertyMetadata(default(int), PropertyChangedCallback));

        /// <summary>
        /// File path dependency property.
        /// </summary>
        public static readonly DependencyProperty FilePathProperty = DependencyProperty.Register(nameof(FilePath),
            typeof(string),
            typeof(SoundPlayer),
            new PropertyMetadata(default(string), PropertyChangedCallback));

        /// <summary>
        /// Initializes a new instance of the <see cref="SoundPlayer"/> class.
        /// </summary>
        public SoundPlayer()
        {
            InitializeComponent();

            Player = new MediaPlayer();
            Player.MediaOpened += PlayerOnMediaOpened;

            Timer = new DispatcherTimer();
            Timer.Tick += TimerOnTick;
        }

        /// <summary>
        /// Gets or sets the exception handler.
        /// </summary>
        public static ExceptionHandlerDelegate ExceptionHandler { get; set; }

        /// <summary>
        /// Gets or sets path to file.
        /// </summary>
        public string FilePath
        {
            get { return (string)GetValue(FilePathProperty); }
            set { SetValue(FilePathProperty, value); }
        }

        /// <summary>
        /// Gets or sets player position near modifier.
        /// </summary>
        public int NearModifier
        {
            get { return (int)GetValue(NearModifierProperty); }
            set { SetValue(NearModifierProperty, value); }
        }

        /// <summary>
        /// Gets or sets player position far modifier.
        /// </summary>
        public int FarModifier
        {
            get { return (int)GetValue(FarModifierProperty); }
            set { SetValue(FarModifierProperty, value); }
        }

        /// <summary>
        /// Gets player instance.
        /// </summary>
        private MediaPlayer Player { get; }

        /// <summary>
        /// Gets timer instance.
        /// </summary>
        private DispatcherTimer Timer { get; }

        /// <summary>
        /// Indicates whether player is paused.
        /// </summary>
        private bool IsPaused { get; set; }

        /// <summary>
        /// Handles property updates.
        /// </summary>
        /// <param name="dependencyObject">Event sender.</param>
        /// <param name="args">Event args.</param>
        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is SoundPlayer ctrl && !string.IsNullOrWhiteSpace(args.Property?.Name))
            {
                switch (args.Property.Name)
                {
                    case nameof(FilePath):
                    {
                        string path = args.NewValue?.ToString();

                        if (!string.IsNullOrWhiteSpace(path) && System.IO.File.Exists(path))
                        {
                            try
                            {
                                ctrl.Player.Open(new Uri(path));
                            }
                            catch (Exception ex)
                            {
                                ExceptionHandler?.Invoke(ex);
                            }
                        }

                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Player opened media event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void PlayerOnMediaOpened(object sender, EventArgs e)
        {
            Opened();
        }

        /// <summary>
        /// Timer tick event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="eventArgs">Event args.</param>
        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            if (ProgressSlider != null && Player != null)
            {
                UpdateProgress();
            }
        }

        #region Progress

        /// <summary>
        /// Progress value changed by mouse wheel.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void ProgressSlider_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            int modifier = e.Delta > 0
                    ? FarModifier
                    : -FarModifier
                ;

            ModifySeekValue(modifier);
            UpdateProgress();

            e.Handled = true;
        }

        /// <summary>
        /// Progress value changed.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="args">Event args.</param>
        private void ProgressSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            int seekValue = (args.OldValue < args.NewValue ? 1 : -1) * (int)args.NewValue;

            SetSeekValue(seekValue);
        }

        #endregion

        #region Volume

        /// <summary>
        /// Volume changed by mouse wheel event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void VolumeSlider_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            int modifier = e.Delta > 0
                    ? VolumeSlider.Interval
                    : -VolumeSlider.Interval
                ;

            ModifyVolume(modifier);

            e.Handled = true;
        }

        /// <summary>
        /// Volume changed event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void VolumeSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetVolume();
        }

        #endregion

        #region Buttons events

        /// <summary>
        /// Backward far on click event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void BackwardFar_OnClick(object sender, RoutedEventArgs e)
        {
            ModifySeekValue(-FarModifier);
        }

        /// <summary>
        /// Backward far on click event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void BackwardNear_OnClick(object sender, RoutedEventArgs e)
        {
            ModifySeekValue(-NearModifier);
        }

        /// <summary>
        /// Stop on click event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void Stop_OnClick(object sender, RoutedEventArgs e)
        {
            StopPlaying();
        }

        /// <summary>
        /// Play on click event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void Play_OnClick(object sender, RoutedEventArgs e)
        {
            StartPlaying();
        }

        /// <summary>
        /// Pause on click event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void Pause_OnClick(object sender, RoutedEventArgs e)
        {
            PausePlaying();
        }

        /// <summary>
        /// Forward far on click event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void ForwardNear_OnClick(object sender, RoutedEventArgs e)
        {
            ModifySeekValue(NearModifier);
        }

        /// <summary>
        /// Forward far on click event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void ForwardFar_OnClick(object sender, RoutedEventArgs e)
        {
            ModifySeekValue(FarModifier);
        }

        #endregion

        #region Playback control

        /// <summary>
        /// Sets progress values to starting values.
        /// </summary>
        private void Opened()
        {
            if (ProgressSlider != null && IsSeekingPossible())
            {
                ProgressSlider.Minimum = 0;
                ProgressSlider.Value = 0;
                ProgressSlider.Maximum = GetMaximumSeekValue().TotalSeconds;
            }
        }

        /// <summary>
        /// Updates progress slider and text.
        /// </summary>
        private void UpdateProgress()
        {
            if (ProgressSlider != null && Player != null)
            {
                ProgressSlider.ValueChanged -= ProgressSlider_OnValueChanged;
                ProgressSlider.Value = Player.Position.TotalSeconds;
                ProgressSlider.ValueChanged += ProgressSlider_OnValueChanged;
            }

            UpdateProgressText();
        }

        /// <summary>
        /// Returns maximum player position.
        /// </summary>
        /// <returns>Player maximum position or <see cref="TimeSpan.Zero"/>.</returns>
        private TimeSpan GetMaximumSeekValue()
        {
            if (IsSeekingPossible())
            {
                return Player.NaturalDuration.TimeSpan;
            }

            return TimeSpan.Zero;
        }

        /// <summary>
        /// Returns current player position.
        /// </summary>
        /// <returns>Player position or <see cref="TimeSpan.Zero"/>.</returns>
        private TimeSpan GetSeekValue()
        {
            if (IsSeekingPossible())
            {
                return Player.Position;
            }

            return TimeSpan.Zero;
        }

        /// <summary>
        /// Sets player position to <paramref name="modifier"/>.
        /// </summary>
        /// <param name="modifier">New position.</param>
        private void SetSeekValue(int modifier)
        {
            if (Player != null)
            {
                Player.Position = CreateSeekValue(modifier);
            }
        }

        /// <summary>
        /// Modifies player position by <paramref name="modifier"/>.
        /// </summary>
        /// <param name="modifier">Position modifier.</param>
        private void ModifySeekValue(int modifier)
        {
            if (Player != null)
            {
                Player.Position += CreateSeekValue(modifier);
            }
        }

        /// <summary>
        /// Creates seek value.
        /// </summary>
        /// <param name="modifier"></param>
        /// <returns></returns>
        private TimeSpan CreateSeekValue(int modifier)
        {
            if (IsSeekingPossible())
            {
                return TimeSpan.FromSeconds(modifier);
            }

            return TimeSpan.Zero;
        }

        /// <summary>
        /// Determines wether seeking is possible.
        /// </summary>
        /// <returns></returns>
        private bool IsSeekingPossible()
        {
            return Player != null
                && Player.NaturalDuration.HasTimeSpan
                ;
        }

        /// <summary>
        /// Pauses player.
        /// </summary>
        private void PausePlaying()
        {
            if (Player != null)
            {
                Player.Pause();
                IsPaused = true;
            }
        }

        /// <summary>
        /// Starts player and timer.
        /// </summary>
        private void StartPlaying()
        {
            if (Player != null)
            {
                if (!IsPaused)
                {
                    Player.Stop();
                    Player.Position = TimeSpan.Zero;
                }
                else
                {
                    IsPaused = false;
                }

                SetVolume();

                Player.Play();

                StartTimer();
            }
        }

        /// <summary>
        /// Stops player and update timer.
        /// </summary>
        private void StopPlaying()
        {
            if (Player != null)
            {
                Timer.Stop();
                Player.Stop();

                Player.Position = TimeSpan.Zero;
                IsPaused = false;

                UpdateProgress();
            }
        }

        #endregion

        #region Timer control

        /// <summary>
        /// Starrs update timer.
        /// </summary>
        private void StartTimer()
        {
            if (Timer != null)
            {
                Timer.Stop();

                Timer.Interval = TimeSpan.FromMilliseconds(100.0);

                Timer.Start();
            }
        }

        #endregion

        #region Volume control

        /// <summary>
        /// Sets volume.
        /// </summary>
        private void SetVolume()
        {
            if (Player != null)
            {
                Player.Volume = VolumeSlider.Value / 100;
            }
        }

        /// <summary>
        /// Modifies volume by <paramref name="modifier"/>.
        /// </summary>
        /// <param name="modifier">Volume modifier.</param>
        private void ModifyVolume(int modifier)
        {
            if (VolumeSlider != null)
            {
                VolumeSlider.Value += modifier;
            }

            SetVolume();
        }

        #endregion

        #region Text control

        /// <summary>
        /// Updates progress text.
        /// </summary>
        private void UpdateProgressText()
        {
            StringBuilder sbText = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(FilePath))
            {
                sbText.AppendFormat("{0:hh\\:mm\\:ss} / {1:hh\\:mm\\:ss} - {2}", GetSeekValue(), GetMaximumSeekValue(), System.IO.Path.GetFileName(FilePath));
            }

            InfoLabel.Content = sbText.ToString();
        }

        #endregion
    }
}
