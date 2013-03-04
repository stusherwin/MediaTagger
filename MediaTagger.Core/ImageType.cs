using System.Drawing.Imaging;

namespace MediaTagger.Core
{
    public class ImageType : MediaFileType
    {
        public ImageFormat Format { get; private set; }

        public ImageType(string name, string extension, string contentType, ImageFormat format)
            : base(name, extension, contentType, MediaType.Image)
        {
            Format = format;
        }

        public static ImageType Jpeg = new ImageType("Jpeg", ".jpg", "image/jpeg", ImageFormat.Jpeg);
        public static ImageType Gif = new ImageType("Gif", ".gif", "image/gif", ImageFormat.Gif);
        public static ImageType Png = new ImageType("Png", ".png", "image/png", ImageFormat.Png);

        public static new ImageType[] All = new[] { Jpeg, Gif, Png };
    }
}
