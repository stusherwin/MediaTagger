using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Test
{
    public class MediaFile
    {
        private FileInfo _fileInfo;

        public MediaFileType MediaFileType { get; private set; }
        public ICollection<Tag> Tags { get; private set; }

        public string Name { get { return System.IO.Path.GetFileNameWithoutExtension(_fileInfo.Name); } }
        public string Path { get { return _fileInfo.FullName; } }
        public DateTime LastModified { get { return _fileInfo.LastWriteTime; } }
        public FileSize Size { get { return new FileSize(_fileInfo.Length); } }

        public MediaFile(FileInfo fileInfo, MediaFileType mediaFileType)
        {
            _fileInfo = fileInfo;
            MediaFileType = mediaFileType;
        }
    }
}
