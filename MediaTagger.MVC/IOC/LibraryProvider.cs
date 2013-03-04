using MediaTagger.Core.Xml;
using MediaTagger.Core;
using MediaTagger.Mvc.Configuration;

namespace MediaTagger.Mvc.IOC
{
    public class LibraryProvider : IProvider<Library>
    {
        readonly LibraryXmlConverter _converter;
        readonly LibrarySettings _settings;

        public LibraryProvider(LibraryXmlConverter converter, LibrarySettings settings)
        {
            _converter = converter;
            _settings = settings;
        }

        public Library Get()
        {
            return _converter.ReadFromFile(_settings.LibraryFile);
        }
    }
}