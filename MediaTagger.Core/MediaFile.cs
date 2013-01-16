using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MediaTagger.Core
{
    public class MediaFile
    {
        public int Id { get; private set; }
        public MediaFileType MediaFileType { get; private set; }
        public ICollection<Tag> Tags { get; private set; }
        public string Name { get; private set; }
        public string Path { get; private set; }
        public DateTime LastModified { get; private set; }
        public FileSize Size { get; private set; }
        public TimeSpan Duration { get; set; }

        public MediaFile(int id, string name, string path, DateTime lastModified, FileSize size, TimeSpan duration, MediaFileType type, ICollection<Tag> tags)
        {
            Id = id;
            Name = name;
            Path = path;
            LastModified = lastModified;
            Size = size;
            Duration = duration;
            MediaFileType = type;
            Tags = tags;
        }
    }
}
