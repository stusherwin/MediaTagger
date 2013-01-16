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
        private Library _library;

        public FilesController(Library library)
        {
            _library = library;
        }

        public FilesOutputModel Get_files()
        {
            return new FilesOutputModel
            {
                Files = _library.Files.Select(f => f.Path).ToArray()
            };
        }
    }
}