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
using System.IO;
using System.Threading;
using MediaTagger.Core;

namespace MediaTagger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Library _library;

        public MediaFile TheFile
        {
            get
            {
                return MediaFile.FromFileInfo(new FileInfo(@"C:\Files\flv.flv"), new MediaFileType("Flv", ".flv", MediaType.Video));
            }
        }

        public List<MediaFile> TheData
        {
            get
            {
                //return Directory.GetFiles(@"C:\Files", "*.jpg")
                //    .Select(f => new MediaFile(new FileInfo(f), new MediaFileType("jpg", ".jpg", MediaType.Image)))
                //    .ToList();
                return _library.GetAllFiles(MediaType.Video, SortOrder.LastModified(OrderDirection.Descending)).ToList();
            }
        }

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(ImageSource), typeof(MainWindow));

        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public MainWindow()
        {

            _library = new Library
            (
                new[] 
                {
                    new LibraryFolder(@"C:\Files")
                },
                new MediaFile[] {}
            );

            InitializeComponent();
            
            var player = new MediaPlayer { Volume = 0, ScrubbingEnabled = true };

            player.Open(new Uri(@"C:\Files\flv.flv"));
            player.Pause();
            //player.Position = TimeSpan.FromSeconds(64);

            while(!player.NaturalDuration.HasTimeSpan)
                Thread.Sleep(100);

            player.Position = TimeSpan.FromMilliseconds(player.NaturalDuration.TimeSpan.TotalMilliseconds / 2);

            //Thread.Sleep(100);

            var width = player.NaturalVideoWidth;
            var height = player.NaturalVideoHeight;

            var rtb = new RenderTargetBitmap(
                width, height, 96, 96, PixelFormats.Pbgra32);
            DrawingVisual dv = new DrawingVisual();

            using (DrawingContext dc = dv.RenderOpen())
                dc.DrawVideo(player, new Rect(0, 0, width, height));

            rtb.Render(dv);
            Source = rtb;
        }
    }
}
