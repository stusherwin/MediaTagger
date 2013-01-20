using System.Linq;
using System.Web;
using MediaTagger.Core;
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

            Duration defaultDuration = GetDefaultThumbnailDuration(videoFile);

            return GenerateOutput(videoFile, defaultDuration);
        }

        public ThumbnailOutputModel get_Thumbnail_FileId_ThumbnailDuration(ThumbnailInputModel model)
        {
            var videoFile = _library.Files.FirstOrDefault(f => f.Id == model.FileId);

            if (videoFile == null)
                throw new HttpException(404, "Not found");

            return GenerateOutput(videoFile, model.ThumbnailDuration);
        }

        private ThumbnailOutputModel GenerateOutput(MediaFile videoFile, Duration thumbnailDuration)
        {
            var thumbnail = _generator.Generate(videoFile, thumbnailDuration);

            return new ThumbnailOutputModel(thumbnail);
        }

        private Duration GetDefaultThumbnailDuration(MediaFile videoFile)
        {
            return videoFile.Duration.GetPercentage(_settings.DefaultThumbnailTimePercentage);
        }
    }
}