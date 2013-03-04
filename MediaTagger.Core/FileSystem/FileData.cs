using System;
using System.IO;

namespace MediaTagger.Core.FileSystem
{
    public class FileData
    {
        public string Name;
        public string Path;
        public string Extension;
        public FileSize Size;
        public DateTime LastModified;

        public static FileData FromFileInfo(FileInfo f)
        {
            return new FileData
                       {
                           Name = System.IO.Path.GetFileNameWithoutExtension(f.Name),
                           Path = f.Name,
                           Extension = System.IO.Path.GetExtension(f.Name),
                           Size = new FileSize(f.Length),
                           LastModified = f.LastWriteTime
                       };
        }
    }
}