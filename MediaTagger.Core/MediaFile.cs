using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MediaTagger.Core
{
    public class MediaFile
    {
        public MediaFileType MediaFileType { get; private set; }
        public ICollection<Tag> Tags { get; private set; }
        public string Name { get; private set; }
        public string Path { get; private set; }
        public DateTime LastModified { get; private set; }
        public FileSize Size { get; private set; }

        public MediaFile(string name, string path, DateTime lastModified, FileSize size, MediaFileType type, ICollection<Tag> tags)
        {
            Name = name;
            Path = path;
            LastModified = lastModified;
            Size = size;
            MediaFileType = type;
            Tags = tags;
        }

        public static MediaFile FromFileInfo(FileInfo fileInfo, MediaFileType mediaFileType)
        {
            return new MediaFile(
                System.IO.Path.GetFileNameWithoutExtension(fileInfo.Name),
                fileInfo.FullName,
                fileInfo.LastWriteTime,
                new FileSize(fileInfo.Length),
                mediaFileType,
                new List<Tag>()
            );
        }
    }
}
