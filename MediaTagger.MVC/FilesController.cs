using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MediaTagger.Core.Xml;
using MediaTagger.Core;

namespace MediaTagger.Server
{
    public class FilesController
    {
        private LibrarySettings _settings;
        private LibraryXmlConverter _libraryConverter;

        public FilesController(LibrarySettings settings, LibraryXmlConverter libraryConverter)
        {
            _settings = settings;
            _libraryConverter = libraryConverter;
        }

        public FilesOutputModel Get_files()
        {
            var library = _libraryConverter.ReadFromFile(_settings.LibraryFile);
            var files = library.GetAllFiles(MediaType.Image, SortOrder.LastModified(OrderDirection.Descending));

            return new FilesOutputModel
            {
                Files = files.Select(f => f.Path).ToArray()
            };
        }
    }
}