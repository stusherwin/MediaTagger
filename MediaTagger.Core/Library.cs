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
    }
}
