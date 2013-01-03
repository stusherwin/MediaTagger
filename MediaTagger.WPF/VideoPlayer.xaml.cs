using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MediaTagger
{
    /// <summary>
    /// Interaction logic for VideoPlayer.xaml
    /// </summary>
    public partial class VideoPlayer : UserControl
    {
        private bool _fullscreenMode;
        private bool _ended;

        public event EventHandler FullscreenModeEntered;
        public event EventHandler FullscreenModeExited;

        public VideoPlayer()
        {
            InitializeComponent();
            FullscreenModeEntered += (_, __) => { };
            FullscreenModeExited += (_, __) => { };

            Player.Loaded += (_, __) => PlayVideo();
            Player.MediaEnded += (_, __) => VideoEnded();
            Player.MediaOpened += (_, __) => VideoLoaded();

            Play.Click += (_, __) => PlayVideo();
            Pause.Click += (_, __) => PauseVideo();
            Stop.Click += (_, __) => StopVideo();
            Full.Click += (_, __) => ToggleFullscreenMode();

            this.KeyDown += (_, e) => HandleKeyPress(e.Key);
        }

        private void HandleKeyPress(Key key)
        {
            if (key == Key.Escape && _fullscreenMode)
                ExitFullscreenMode();
        }

        private void VideoLoaded()
        {
            PlayPosition.Maximum = Player.NaturalDuration.TimeSpan.TotalMilliseconds;
        }

        private void PlayVideo()
        {
            if (_ended)
            {
                Player.Stop();
                _ended = false;
            }

            Player.Play();
        }

        private void PauseVideo()
        {
            Player.Pause();
        }

        private void StopVideo()
        {
            Player.Stop();
            Player.Close();
        }

        private void VideoEnded()
        {
            _ended = true;
            Player.Pause();
        }

        private void UpdatePlayPosition(double newPosition)
        {
            Player.Position = TimeSpan.FromMilliseconds(newPosition);
        }

        private void ToggleFullscreenMode()
        {
            if (_fullscreenMode)
            {
                ExitFullscreenMode();
            }
            else
            {
                EnterFullscreenMode();
            }
        }

        private void EnterFullscreenMode()
        {
            _fullscreenMode = true;
            FullscreenModeEntered(this, EventArgs.Empty);
        }

        private void ExitFullscreenMode()
        {
            _fullscreenMode = false;
            FullscreenModeExited(this, EventArgs.Empty);
        }

        bool _dragging;
        private void PlayPosition_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            UpdatePlayPosition(PlayPosition.Value);
            _dragging = false;
        }

        private void PlayPosition_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            _dragging = true;
        }

        DispatcherTimer _timer;
        private void PlayPosition_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!_dragging)
            {
                if (_timer == null)
                {
                    _timer = new DispatcherTimer(
                        TimeSpan.FromMilliseconds(300),
                        DispatcherPriority.Input,
                        (timer, __) =>
                        {
                            UpdatePlayPosition(PlayPosition.Value);
                            (timer as DispatcherTimer).Stop();
                            _timer = null;
                        }, 
                        Dispatcher);
                    _timer.Start();
                }
                else
                {
                    _timer.Stop();
                    _timer.Start();
                }
            }
        }
    }
}
