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
            var videoFile = _library.Files.FirstOrDefault(f => f.Id == model.FileId);

            if (videoFile == null)
                throw new HttpException(404, "Not found");

            return GenerateOutput(videoFile, null, null);
        }

        public ThumbnailOutputModel get_Thumbnail_FileId_ThumbnailDuration(ThumbnailInputModel model)
        {
            var videoFile = _library.Files.FirstOrDefault(f => f.Id == model.FileId);

            if (videoFile == null)
                throw new HttpException(404, "Not found");

            return GenerateOutput(videoFile, model.ThumbnailDuration, null);
        }

        private ThumbnailOutputModel GenerateOutput(MediaFile videoFile, Duration pointInTime, ImageSize imageSize)
        {
            var options = GetOptionsWithDefaults(videoFile, pointInTime, imageSize);
            var thumbnail = _generator.Generate(videoFile, options);

            // sleep to increase server round-trip time for testing
            Thread.Sleep(1000);

            return new ThumbnailOutputModel(thumbnail);
        }

        private ThumbnailGenerationOptions GetOptionsWithDefaults(MediaFile videoFile, Duration pointInTime, ImageSize imageSize)
        {
            return new ThumbnailGenerationOptions(
                pointInTime ?? videoFile.Duration.GetPercentage(_settings.DefaultThumbnailTimePercentage),
                imageSize ?? new ImageSize(_settings.DefaultThumbnailWidth, _settings.DefaultThumbnailHeight)
            );
        }
    }
}