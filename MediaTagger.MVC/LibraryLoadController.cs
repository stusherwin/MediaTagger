using MediaTagger.Core;
using MediaTagger.Core.Xml;
using MediaTagger.Mvc.Configuration;

namespace MediaTagger.Mvc
{
    //public class LibraryLoadController
    //{
    //    private readonly LibraryFolderLoader _folderLoader;
    //    private readonly LibraryXmlConverter _xml;
    //    private readonly LibrarySettings _settings;

    //    public LibraryLoadController(LibraryFolderLoader folderLoader, LibraryXmlConverter xml, LibrarySettings settings)
    //    {
    //        _folderLoader = folderLoader;
    //        _xml = xml;
    //        _settings = settings;
    //    }

    //    public string get_LoadLibrary()
    //    {
    //        var lib = _folderLoader.Load(new[] { new LibraryFolder(@"C:\Files", TODO) }, MediaType.Video);
    //        _xml.WriteToFile(lib, _settings.LibraryFile);

    //        return "";
    //    }
    //}
}