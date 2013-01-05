using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MediaTagger.Core
{
    public class LibraryFolder
    {
        public string Path { get; private set; }

        public LibraryFolder(string path)
        {
            Path = path;
        }

        public IEnumerable<MediaFile> GetFiles(IEnumerable<MediaFileType> types)
        {
            return
                from t in types
                from f in new DirectoryInfo(Path).GetFiles("*." + t.Extension, SearchOption.AllDirectories)
                select MediaFile.FromFileInfo(f, t);
        }
    }
}
