using MediaTagger.Core;
using MediaTagger.Core.Xml;
using MediaTagger.Mvc.Configuration;

namespace MediaTagger.Mvc
{
    public class LibraryLoadController
    {
        private readonly LibraryLoader _loader;
        private readonly LibraryXmlConverter _xml;
        private readonly LibrarySettings _settings;

        public LibraryLoadController(LibraryLoader loader, LibraryXmlConverter xml, LibrarySettings settings)
        {
            _loader = loader;
            _xml = xml;
            _settings = settings;
        }

        public string get_LoadLibrary()
        {
            var lib = _loader.Load(new[] { new LibraryFolder(@"C:\Files") }, MediaType.Video);
            _xml.WriteToFile(lib, _settings.LibraryFile);

            return "";
        }
    }
}