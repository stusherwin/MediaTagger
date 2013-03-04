using System;
using System.Linq;

namespace MediaTagger.Core.Thumbnails
{
    public class BulkThumbnailGenerator
    {
        private static Library _library;
        private static SizedThumbnailGenerator _thumbnailGenerator;

        public event EventHandler<ThumbnailGeneratedEventArgs> ThumbnailGenerated;

        public BulkThumbnailGenerator(Library library, SizedThumbnailGenerator thumbnailGenerator)
        {
            _library = library;
            _thumbnailGenerator = thumbnailGenerator;

            ThumbnailGenerated += (_, __) => { };
        }

        public void GenerateThumbnails(ThumbnailGenerationOptions options)
        {
            var i = 0;
            var count = _library.Files.All.Count();
            foreach (var file in _library.Files.All)
            {
                _thumbnailGenerator.Generate(file, options);

                ThumbnailGenerated(this, new ThumbnailGeneratedEventArgs(file, ++i, count));
            }
        }
    }
}