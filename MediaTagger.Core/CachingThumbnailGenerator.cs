using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace MediaTagger.Core
{
    public class FileSystemCachedThumbnailGenerator : IThumbnailGenerator
    {
        private readonly IThumbnailGenerator _thumbnailGenerator;
        private readonly string _thumbnailLocation;

        public FileSystemCachedThumbnailGenerator(IThumbnailGenerator thumbnailGenerator, string thumbnailLocation)
        {
            _thumbnailGenerator = thumbnailGenerator;
            _thumbnailLocation = thumbnailLocation;
        }

        public ThumbnailImage Generate(MediaFile videoFile, Duration thumbnailDuration)
        {
            if(!CachedFileExists(videoFile, thumbnailDuration))
                CreateCachedFile(videoFile, thumbnailDuration);

            return GetCachedFile(videoFile, thumbnailDuration);
        }

        private bool CachedFileExists(MediaFile videoFile, Duration thumbnailTime)
        {
            var thumbnailFile = GetThumbnailFile(videoFile, thumbnailTime);

            return File.Exists(thumbnailFile);
        }

        private void CreateCachedFile(MediaFile videoFile, Duration thumbnailTime)
        {
            var thumbnail = _thumbnailGenerator.Generate(videoFile, thumbnailTime);
            var thumbnailFile = GetThumbnailFile(videoFile, thumbnailTime);
            
            if (!Directory.Exists(_thumbnailLocation))
                Directory.CreateDirectory(_thumbnailLocation);
            
            thumbnail.Image.Save(thumbnailFile);
        }

        private ThumbnailImage GetCachedFile(MediaFile videoFile, Duration thumbnailTime)
        {
            var thumbnailFile = GetThumbnailFile(videoFile, thumbnailTime);

            return new ThumbnailImage(Image.FromFile(thumbnailFile), new ImageType(ImageFormat.Png, "image/png"));
        }

        private string GetThumbnailFile(MediaFile videoFile, Duration thumbnailTime)
        {
            return _thumbnailLocation + "\\" + videoFile.Id.ToString() + "_" + thumbnailTime.Value.Ticks + ".png";
        }
    }
}
