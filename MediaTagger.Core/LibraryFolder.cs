using System.Collections.Generic;

namespace MediaTagger.Core
{
    public class LibraryFolder
    {
        public string Path { get; private set; }
        public IEnumerable<MediaFile> Files { get; private set; }

        public LibraryFolder(string path, IEnumerable<MediaFile> files)
        {
            Path = path;
            Files = files;
        }
    }
}
