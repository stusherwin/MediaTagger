using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MediaTagger.Core;

namespace MediaTagger.Mvc
{
    public class MediaController
    {
        private readonly Library _library;

        public MediaController(Library library)
        {
            _library = library;
        }

        public MediaOutputModel get_Media_FileName(MediaInputModel model)
        {
            var id = int.Parse(model.FileName.Split('.')[0]);
            var mediaFile = _library.Files[id];

            if (mediaFile == null)
                throw new HttpException(404, "Not found");

            return new MediaOutputModel(mediaFile);
        }
    }
}