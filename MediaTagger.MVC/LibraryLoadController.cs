using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MediaTagger.Core;
using MediaTagger.Core.Xml;

namespace MediaTagger.Server
{
    public class LibraryLoadController
    {
        private LibraryLoader _loader;
        private LibraryXmlConverter _xml;
        private LibrarySettings _settings;

        public LibraryLoadController(LibraryLoader loader, LibraryXmlConverter xml, LibrarySettings settings)
        {
            _loader = loader;
            _xml = xml;
            _settings = settings;
        }

        public string get_LoadLibrary()
        {
            var lib = _loader.Load(new LibraryFolder[] { new LibraryFolder(@"C:\Files") }, MediaType.Video);
            _xml.WriteToFile(lib, _settings.LibraryFile);

            return "";
        }
    }
}