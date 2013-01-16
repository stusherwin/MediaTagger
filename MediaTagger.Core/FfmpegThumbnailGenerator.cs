using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.IO;
namespace MediaTagger.Core
{
    public class FfmpegThumbnailGenerator : IThumbnailGenerator
    {
        private FfmpegWrapper _ffmpeg;
        private string _tempFileLocation;

        static object _thumbnailLock = new object();

        public FfmpegThumbnailGenerator(FfmpegWrapper ffmpeg, string tempFileLocation)
        {
            _ffmpeg = ffmpeg;
            _tempFileLocation = tempFileLocation;
        }

        public ThumbnailImage Generate(MediaFile videoFile, TimeSpan thumbnailTime)
        {
            string thumbnailFile;

            lock (_thumbnailLock)
            {
                thumbnailFile = CreateUniqueFile(_tempFileLocation, "tmp", ".png");
            }
            var thumbnail = GenerateThumbnail(videoFile, thumbnailTime, thumbnailFile);
            //File.Delete(thumbnailFile);

            return thumbnail;
        }

        private ThumbnailImage GenerateThumbnail(MediaFile videoFile, TimeSpan thumbnailTime, string thumbnailFile)
        {
            _ffmpeg.CreateThumbnailImage(videoFile.Path, RestrictToVideoDuration(thumbnailTime, videoFile), thumbnailFile);
            var image = Image.FromFile(thumbnailFile);
            return new ThumbnailImage(image, new ImageType(ImageFormat.Png, "image/png"));
        }

        private static string CreateUniqueFile(string directory, string fileNameBase, string extension)
        {
            string fileName = GetUniqueFileName(directory, fileNameBase, extension);
                
            using(var fs = File.Create(fileName))
                fs.Close();

            return fileName;
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

        //TODO: move to Duration value object
        private static TimeSpan RestrictToVideoDuration(TimeSpan time, MediaFile videoFile)
        {
            return time > videoFile.Duration 
                ? videoFile.Duration 
                : time;
        }
    }
}
