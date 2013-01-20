using System.Linq;
using MediaTagger.Core;

namespace MediaTagger.Mvc
{
    public class FilesController
    {
        private readonly Library _library;

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