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
using System.Windows.Shapes;

namespace MediaTagger
{
    /// <summary>
    /// Interaction logic for VideoPopupWindow.xaml
    /// </summary>
    public partial class VideoPopupWindow : Window
    {
        public VideoPopupWindow()
        {
            InitializeComponent();

            VideoPlayer.FullscreenModeEntered += (_, __) => EnterFullScreen();
            VideoPlayer.FullscreenModeExited += (_, __) => ExitFullScreen();
        }

        private void EnterFullScreen()
        {
            this.WindowStyle = System.Windows.WindowStyle.None;
            this.WindowState = System.Windows.WindowState.Maximized;
        }

        private void ExitFullScreen()
        {
            this.WindowStyle = System.Windows.WindowStyle.SingleBorderWindow;
            this.WindowState = System.Windows.WindowState.Normal;
        }
    }
}
