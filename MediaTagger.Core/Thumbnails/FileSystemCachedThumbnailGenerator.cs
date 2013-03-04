using System.Drawing;
using MediaTagger.Core.FileSystem;

namespace MediaTagger.Core.Thumbnails
{
    public abstract class FileSystemCachedThumbnailGenerator : IThumbnailGenerator
    {
        private readonly FileManager _fileManager;
        private readonly string _cacheLocation;

        public FileSystemCachedThumbnailGenerator(FileManager fileManager, string cacheLocation)
        {
            _fileManager = fileManager;
            _cacheLocation = cacheLocation;
        }

        public ThumbnailImage Generate(MediaFile videoFile, ThumbnailGenerationOptions options)
        {
            return new ThumbnailImage(GetImage(videoFile, options), options.ImageType);
        }

        protected abstract string GenerateKey(MediaFile videoFile, ThumbnailGenerationOptions options);
        protected abstract Image GenerateImage(MediaFile videoFile, ThumbnailGenerationOptions options);

        private Image GetImage(MediaFile videoFile, ThumbnailGenerationOptions options)
        {
            var key = GenerateKey(videoFile, options);
            var file = _cacheLocation + "\\" + key + options.ImageType.Extension;

            var image = LoadImage(file);

            if (image == null)
            {
                image = GenerateImage(videoFile, options);
                SaveImage(image, file);                
            }

            return image;
        }

        private Image LoadImage(string file)
        {
            if (_fileManager.FileExists(file))
                return _fileManager.LoadImage(file);

            return null;
        }

        private void SaveImage(Image image, string file)
        {
            _fileManager.EnsureDirectoryExists(_cacheLocation);
            image.Save(file);
        }
    }
}