using System.Drawing.Imaging;

namespace MediaTagger.Core
{
    public class ImageType
    {
        public ImageFormat Format;
        public string ContentType;

        public ImageType(ImageFormat format, string contentType)
        {
            Format = format;
            ContentType = contentType;
        }
    }
}
