using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaTagger.Core
{
    public class Library
    {
        public LibraryFolder[] Folders { get; private set; }
        public MediaFile[] Files { get; private set; }

        public Library(LibraryFolder[] folders, MediaFile[] files)
        {
            Folders = folders;
            Files = files;
        }

        public IEnumerable<MediaFile> GetAllFiles()
        {
            return Folders.SelectMany(lf => lf.GetFiles(MediaFileType.All));
        }

        public IEnumerable<MediaFile> GetAllFiles(MediaType mediaType, SortOrder<MediaFile> sortOrder)
        {
            var fileTypes = MediaFileType.All.Where(ft => ft.MediaType == mediaType);

            var files = Folders
                .SelectMany(lf => lf.GetFiles(fileTypes));

            return sortOrder.Evaluate(files);
        }
    }
}
