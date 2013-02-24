using System.Drawing;
using System.IO;

namespace MediaTagger.Core.Thumbnails
{
    public class FileSystemImageCache
    {
        private const string IMAGE_FILE_EXTENSION = ".png";

        private readonly string _cacheLocation;

        public FileSystemImageCache(string cacheLocation)
        {
            _cacheLocation = cacheLocation;
        }

        public bool ContainsKey(string key)
        {
            return File.Exists(GetFilePath(key));
        }

        public void Add(string key, Image image)
        {
            if (!Directory.Exists(_cacheLocation))
                Directory.CreateDirectory(_cacheLocation);

            image.Save(GetFilePath(key));
        }

        public Image Get(string key)
        {
            return LoadImageWithoutLockingFile(GetFilePath(key));
        }

        private string GetFilePath(string key)
        {
            return _cacheLocation + "\\" + key + IMAGE_FILE_EXTENSION;
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
    }
}