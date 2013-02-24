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
                files = _library.Files
                    .Take(100)
                    .Select(f => new FilesOutputModel.FileModel
                    {
                        id = f.Id,
                        title = f.Name,
                        description = "This is a description from the server.",
                        contentType = f.MediaFileType.ContentType,
                        mediaUrl = "/media/" + f.Id + "." + f.MediaFileType.Extension,
                        thumbnailUrl = "/thumbnail/" + f.Id
                    })
                    .ToArray()
            };
        }
    }
}