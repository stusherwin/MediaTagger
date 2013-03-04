using System.Collections.Generic;
using System.Linq;

namespace MediaTagger.Core
{
    public class Library
    {
        private readonly List<LibraryFolder> _folders;

        public FileCollection Files { get; private set; }
        public IEnumerable<LibraryFolder> Folders { get { return _folders; } }

        public Library(IEnumerable<LibraryFolder> folders)
        {
            _folders = folders.ToList();
            Files = new FileCollection(_folders);
        }

        public void AddFolder(string folderPath, LibraryFolderLoader loader)
        {
            if (_folders.Any(f => f.Path == folderPath))
                return;

            var folder = loader.Load(folderPath);
            _folders.Add(folder);
        }
    }
}
