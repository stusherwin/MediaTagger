using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace MediaTagger.Core.FileSystem
{
    public class FileManager
    {
        public Image LoadImage(string imageFile)
        {
            return LoadImageWithoutLockingFile(imageFile);
        }

        private static Image LoadImageWithoutLockingFile(string imageFile)
        {
            using (var fs = new FileStream(imageFile, FileMode.Open, FileAccess.Read))
            {
                using (var image = Image.FromStream(fs))
                {
                    return (Image)image.Clone();
                }
            }
        }

        public string FindUniqueFileName(string directory, string fileNameBase, string extension)
        {
            EnsureDirectoryExists(directory);

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

        public void CreateFile(string fileName)
        {
            using (var fs = File.Create(fileName))
                fs.Close();
        }

        public bool FileExists(string file)
        {
            return File.Exists(file);
        }

        public void EnsureFileDeleted(string file)
        {
            if (File.Exists(file))
                File.Delete(file);
        }

        public void EnsureDirectoryExists(string directory)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }

        public IEnumerable<FileData> GetAllFileData(string folderPath, IEnumerable<string> extensions)
        {
            return from ext in extensions
                   from fileInfo in GetAllFileData(folderPath, ext)
                   select fileInfo;
        }

        public IEnumerable<FileData> GetAllFileData(string folderPath, string extension)
        {
            return new DirectoryInfo(folderPath)
                .GetFiles("*" + extension, SearchOption.AllDirectories)
                .Select(f => FileData.FromFileInfo(f));
        }
    }
}