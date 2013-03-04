using System.Linq;
using System.Threading;
using System.Web;
using MediaTagger.Core;
using MediaTagger.Core.Thumbnails;
using MediaTagger.Mvc.Configuration;

namespace MediaTagger.Mvc
{
    public class ThumbnailController
    {
        private readonly Library _library;
        private readonly IThumbnailGenerator _generator;
        private readonly ThumbnailGeneratorSettings _settings;

        public ThumbnailController(Library library, IThumbnailGenerator generator, ThumbnailGeneratorSettings settings)
        {
            _library = library;
            _generator = generator;
            _settings = settings;
        }

        public ThumbnailOutputModel get_Thumbnail_FileId(ThumbnailInputModel model)
        {
            var videoFile = _library.Files[model.FileId];

            if (videoFile == null)
                throw new HttpException(404, "Not found");

            return GenerateOutput(videoFile);
        }

        private ThumbnailOutputModel GenerateOutput(MediaFile videoFile)
        {
            var options = GetOptions();
            var thumbnail = _generator.Generate(videoFile, options);

            return new ThumbnailOutputModel(thumbnail);
        }

        private ThumbnailGenerationOptions GetOptions()
        {
            return new ThumbnailGenerationOptions(
                new PointInTime(new Percentage(_settings.DefaultThumbnailTimePercentage)), 
                (ImageType)MediaFileType.All.FindByName(_settings.DefaultThumbnailImageType),
                new ImageSize(_settings.DefaultThumbnailWidth, _settings.DefaultThumbnailHeight));
        }
    }
}