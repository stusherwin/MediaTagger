using System.Drawing;
using MediaTagger.Core.FileSystem;

namespace MediaTagger.Core.Thumbnails
{
    public class BaseThumbnailGenerator : FileSystemCachedThumbnailGenerator
    {
        private readonly IThumbnailGenerator _thumbnailGenerator;

        public BaseThumbnailGenerator(FileManager fileManager, string cacheLocation, IThumbnailGenerator thumbnailGenerator)
            : base(fileManager, cacheLocation)
        {
            _thumbnailGenerator = thumbnailGenerator;
        }

        protected override string GenerateKey(MediaFile videoFile, ThumbnailGenerationOptions options)
        {
            return string.Format("{0}_{1}",
                videoFile.Id,
                options.PointInTime.ResolveAgainst(videoFile.Duration).ToTicksString());
        }

        protected override Image GenerateImage(MediaFile videoFile, ThumbnailGenerationOptions options)
        {
            return _thumbnailGenerator.Generate(videoFile, options).Image;
        }
    }
}
