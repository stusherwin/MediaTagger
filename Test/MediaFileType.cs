using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
    public class MediaFileType
    {
        public string Name { get; private set; }
        public string Extension { get; private set; }
        public MediaType MediaType { get; private set; }

        public MediaFileType(string name, string extension, MediaType mediaType)
        {
            Name = name;
            Extension = extension;
            MediaType = mediaType;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
