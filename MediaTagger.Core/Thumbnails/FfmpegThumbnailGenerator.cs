using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ImageResizer;

namespace MediaTagger.Core.Thumbnails
{
    public class FfmpegThumbnailGenerator : IThumbnailGenerator
    {
        private const string TEMP_FILE_BASE = "tmp";
        private const string TEMP_FILE_EXTENSION = ".png";
        private static readonly ImageType THUMBNAIL_IMAGE_TYPE = new ImageType(ImageFormat.Png, "image/png");

        private readonly FfmpegWrapper _ffmpeg;
        private readonly string _tempFileLocation;

        private static readonly object _thumbnailLock = new object();

        public FfmpegThumbnailGenerator(FfmpegWrapper ffmpeg, string tempFileLocation)
        {
            _ffmpeg = ffmpeg;
            _tempFileLocation = tempFileLocation;
        }

        public ThumbnailImage Generate(MediaFile videoFile, ThumbnailGenerationOptions options)
        {
            string thumbnailFile = CreateUniqueThumbnailFile();

            try
            {
                return GenerateThumbnail(videoFile, options, thumbnailFile);
            }
            finally
            {
                if(File.Exists(thumbnailFile))
                    File.Delete(thumbnailFile);
            }
        }

        private string CreateUniqueThumbnailFile()
        {
            string file;

            // lock so parallel threads don't get given the same temp file
            lock (_thumbnailLock)
            {
                file = GetUniqueFileName(_tempFileLocation, TEMP_FILE_BASE, TEMP_FILE_EXTENSION);
                CreateFile(file);
            }

            return file;
        }

        private ThumbnailImage GenerateThumbnail(MediaFile videoFile, ThumbnailGenerationOptions options, string thumbnailFile)
        {
            var pointInTime = options.PointInTime.CapAt(videoFile.Duration);
            _ffmpeg.CreateThumbnailImage(videoFile.Path, pointInTime, thumbnailFile);

            var image = LoadImageWithoutLockingFile(thumbnailFile);

            return new ThumbnailImage(image, THUMBNAIL_IMAGE_TYPE);
        }

        //TODO: refactor to separate class
        private Image LoadImageWithoutLockingFile(string imageFile)
        {
            using (var fs = new FileStream(imageFile, FileMode.Open, FileAccess.Read))
            {
                using (var image = Image.FromStream(fs))
                {
                    return (Image)image.Clone();
                }
            }
        }

        private static void CreateFile(string fileName)
        {
            using(var fs = File.Create(fileName))
                fs.Close();
        }

        private static string GetUniqueFileName(string directory, string fileNameBase, string extension)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var tempFiles = Directory.GetFiles(directory, fileNameBase + "*" + extension);
            
            var candidate = directory + "\\" + fileNameBase + extension;
            var suffix = 0;
            while (tempFiles.Contains(candidate))
            {
                suffix++;
                candidate = directory + "\\" + fileNameBase + suffix.ToString() + extension;
            }

            return candidate;
        }
    }
}
