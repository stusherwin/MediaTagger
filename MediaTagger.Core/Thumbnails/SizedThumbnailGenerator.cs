using System.Drawing;
using System.IO;
using ImageResizer;
using MediaTagger.Core.FileSystem;

namespace MediaTagger.Core.Thumbnails
{
    public class SizedThumbnailGenerator : FileSystemCachedThumbnailGenerator
    {
        private readonly IThumbnailGenerator _thumbnailGenerator;

        public SizedThumbnailGenerator(FileManager fileManager, string cacheLocation, IThumbnailGenerator thumbnailGenerator)
            : base(fileManager, cacheLocation)
        {
            _thumbnailGenerator = thumbnailGenerator;
        }

        protected override string GenerateKey(MediaFile videoFile, ThumbnailGenerationOptions options)
        {
            return string.Format("{0}_{1}_{2}",
                videoFile.Id,
                options.PointInTime.ResolveAgainst(videoFile.Duration).ToTicksString(),
                options.ImageSize);
        }

        protected override Image GenerateImage(MediaFile videoFile, ThumbnailGenerationOptions options)
        {
            var baseImage = _thumbnailGenerator.Generate(videoFile, options).Image;

            var sizedImage = ResizeImage(baseImage, options.ImageSize.Width, options.ImageSize.Height);
            return sizedImage;
        }

        private Image ResizeImage(Image image, int width, int height)
        {
            Stream dest = new MemoryStream();
            ImageBuilder.Current.Build(image, dest, new ResizeSettings
            {
                Width = width,
                Height = height,
                Mode = FitMode.Crop
            });

            return Image.FromStream(dest);
        }
    }
}