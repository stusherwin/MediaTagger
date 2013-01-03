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

namespace MediaTagger
{
    /// <summary>
    /// Interaction logic for ThumbnailGrid.xaml
    /// </summary>
    public partial class ThumbnailGrid : UserControl
    {
        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(List<MediaFile>), typeof(ThumbnailGrid));

        public List<MediaFile> Items
        {
            get { return (List<MediaFile>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public ThumbnailGrid()
        {
            InitializeComponent();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new VideoPopupWindow().ShowDialog();
        }
    }
}
