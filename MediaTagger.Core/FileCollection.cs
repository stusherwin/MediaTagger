using System.Collections.Generic;
using System.Linq;

namespace MediaTagger.Core
{
    public class FileCollection
    {
        private readonly List<LibraryFolder> _folders;

        public FileCollection(List<LibraryFolder> folders)
        {
            _folders = folders;
        }

        public MediaFile this[int fileId]
        {
            get { return All.FirstOrDefault(f => f.Id == fileId); }
        }

        public IEnumerable<MediaFile> All
        {
            get { return _folders.SelectMany(f => f.Files); }
        }
    }
}