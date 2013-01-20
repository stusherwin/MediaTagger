namespace MediaTagger.Core
{
    public class LibraryFolder
    {
        public string Path { get; private set; }

        public LibraryFolder(string path)
        {
            Path = path;
        }
    }
}
