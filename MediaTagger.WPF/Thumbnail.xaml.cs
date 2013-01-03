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
using Test;
using System.Threading;

namespace MediaTagger
{
    /// <summary>
    /// Interaction logic for Thumbnail.xaml
    /// </summary>
    public partial class Thumbnail : UserControl
    {
        public static readonly DependencyProperty MediaFileProperty = DependencyProperty.Register("MediaFile", typeof(MediaFile), typeof(Thumbnail));

        public MediaFile MediaFile
        {
            get { return (MediaFile)GetValue(MediaFileProperty); }
            set { SetValue(MediaFileProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(ImageSource), typeof(Thumbnail));

        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        //public ImageSource Image
        //{
        //    get 
        //    {
                
        //        return rtb;// BitmapFrame.Create(rtb).GetCurrentValueAsFrozen();  
        //        //return @"C:\Files\jpg.jpg"; 
        //    }
        //}

        public Thumbnail()
        {
            InitializeComponent();

            new Thread(() =>
            {
                var player = new MediaPlayer { Volume = 0, ScrubbingEnabled = true };

                player.Open(new Uri(MediaFile.Path));
                player.Pause();
                player.Position = TimeSpan.FromSeconds(10);
                Thread.Sleep(3000);

                var width = player.NaturalVideoWidth;
                var height = player.NaturalVideoHeight;

                var rtb = new RenderTargetBitmap(
                    width, height, 96, 96, PixelFormats.Pbgra32);
                DrawingVisual dv = new DrawingVisual();

                using (DrawingContext dc = dv.RenderOpen())
                    dc.DrawVideo(player, new Rect(0, 0, width, height));

                rtb.Render(dv);
                Dispatcher.Invoke(new Action(() =>
                {
                    Source = rtb;
                }));
            }).Start();
        }
    }
}
