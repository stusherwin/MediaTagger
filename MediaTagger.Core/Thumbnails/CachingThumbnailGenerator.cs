using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ImageResizer;

namespace MediaTagger.Core.Thumbnails
{
    public class FileSystemCachedThumbnailGenerator : IThumbnailGenerator
    {
        private static readonly ImageType THUMBNAIL_IMAGE_TYPE = new ImageType(ImageFormat.Png, "image/png");

        private readonly IThumbnailGenerator _thumbnailGenerator;
        private readonly FileSystemImageCache _baseImageCache;
        private readonly FileSystemImageCache _sizedImageCache;

        public FileSystemCachedThumbnailGenerator(IThumbnailGenerator thumbnailGenerator, FileSystemImageCache baseImageCache, FileSystemImageCache sizedImageCache)
        {
            _thumbnailGenerator = thumbnailGenerator;
            _baseImageCache = baseImageCache;
            _sizedImageCache = sizedImageCache;
        }

        public ThumbnailImage Generate(MediaFile videoFile, ThumbnailGenerationOptions options)
        {
            var sizedImage = GetSizedImage(videoFile, options);

            return new ThumbnailImage(sizedImage, THUMBNAIL_IMAGE_TYPE);
        }

        private Image GetSizedImage(MediaFile videoFile, ThumbnailGenerationOptions options)
        {
            var sizedKey = GenerateSizedImageKey(videoFile, options);

            if (_sizedImageCache.ContainsKey(sizedKey))
                return _sizedImageCache.Get(sizedKey);
            
            var baseImage = GetBaseImage(videoFile, options);

            var sizedImage = ResizeImage(baseImage, options.ImageSize.Width, options.ImageSize.Height);
            _sizedImageCache.Add(sizedKey, sizedImage);

            return sizedImage;
        }

        private string GenerateSizedImageKey(MediaFile videoFile, ThumbnailGenerationOptions options)
        {
            return string.Format("{0}_{1}_{2}", 
                                 videoFile.Id, 
                                 options.PointInTime.Value.Ticks, 
                                 options.ImageSize);
        }

        private Image GetBaseImage(MediaFile videoFile, ThumbnailGenerationOptions options)
        {
            var baseKey = GenerateBaseImageKey(videoFile, options);

            if (_baseImageCache.ContainsKey(baseKey))
                return _baseImageCache.Get(baseKey);

            var baseImage = _thumbnailGenerator.Generate(videoFile, options).Image;
            _baseImageCache.Add(baseKey, baseImage);

            return baseImage;
        }

        private string GenerateBaseImageKey(MediaFile videoFile, ThumbnailGenerationOptions options)
        {
            return string.Format("{0}_{1}",
                                 videoFile.Id,
                                 options.PointInTime.Value.Ticks);
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
