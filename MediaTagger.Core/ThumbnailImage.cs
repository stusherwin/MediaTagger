using System.Drawing;

namespace MediaTagger.Core
{
    public class ThumbnailImage
    {
        public Image Image;
        public ImageType Type;

        public ThumbnailImage(Image image, ImageType type)
        {
            Image = image;
            Type = type;
        }
    }
}
