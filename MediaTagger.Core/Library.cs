using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
    public class Library
    {
        public LibraryFolder[] Folders { get; private set; }
        public MediaFileType[] FileTypes { get; private set; }

        public Library(LibraryFolder[] folders, MediaFileType[] fileTypes)
        {
            Folders = folders;
            FileTypes = fileTypes;
        }

        public IEnumerable<MediaFile> GetAllFiles()
        {
            return Folders.SelectMany(lf => lf.GetFiles(FileTypes));
        }

        public IEnumerable<MediaFile> GetAllFiles(MediaType mediaType, SortOrder<MediaFile> sortOrder)
        {
            var fileTypes = FileTypes.Where(ft => ft.MediaType == mediaType);

            var files = Folders
                .SelectMany(lf => lf.GetFiles(fileTypes));

            return sortOrder.Evaluate(files);
        }
    }
}
