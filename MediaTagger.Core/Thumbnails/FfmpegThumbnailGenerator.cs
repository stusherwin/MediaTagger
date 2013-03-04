using MediaTagger.Core.FileSystem;

namespace MediaTagger.Core.Thumbnails
{
    public class FfmpegThumbnailGenerator : IThumbnailGenerator
    {
        private const string TEMP_FILE_NAME_BASE = "tmp";
        private static readonly object _thumbnailLock = new object();

        private readonly FfmpegWrapper _ffmpeg;
        private readonly FileManager _fileManager;
        private readonly string _tempFileLocation;

        public FfmpegThumbnailGenerator(FfmpegWrapper ffmpeg, FileManager fileManager, string tempFileLocation)
        {
            _ffmpeg = ffmpeg;
            _fileManager = fileManager;
            _tempFileLocation = tempFileLocation;
        }

        public ThumbnailImage Generate(MediaFile videoFile, ThumbnailGenerationOptions options)
        {
            string thumbnailFile = CreateUniqueThumbnailFile(TEMP_FILE_NAME_BASE, options.ImageType.Extension);

            try
            {
                return GenerateThumbnail(videoFile, options, thumbnailFile);
            }
            finally
            {
                _fileManager.EnsureFileDeleted(thumbnailFile);
            }
        }

        private string CreateUniqueThumbnailFile(string fileNameBase, string extension)
        {
            string file;

            // lock so parallel threads don't get given the same temp file
            lock (_thumbnailLock)
            {
                file = _fileManager.FindUniqueFileName(_tempFileLocation, fileNameBase, extension);
                _fileManager.CreateFile(file);
            }

            return file;
        }

        private ThumbnailImage GenerateThumbnail(MediaFile videoFile, ThumbnailGenerationOptions options, string thumbnailFile)
        {
            _ffmpeg.CreateThumbnailImage(videoFile.Path, options.PointInTime.ResolveAgainst(videoFile.Duration), thumbnailFile);

            var image = _fileManager.LoadImage(thumbnailFile);

            return new ThumbnailImage(image, options.ImageType);
        }
    }
}
