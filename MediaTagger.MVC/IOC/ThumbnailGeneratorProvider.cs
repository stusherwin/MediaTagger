using MediaTagger.Core;
using MediaTagger.Mvc.Configuration;

namespace MediaTagger.Mvc.IOC
{
    public class ThumbnailGeneratorProvider : IProvider<IThumbnailGenerator>
    {
        readonly FfmpegWrapper _ffmpeg;
        readonly ThumbnailGeneratorSettings _settings;

        public ThumbnailGeneratorProvider(FfmpegWrapper ffmpeg, ThumbnailGeneratorSettings settings)
        {
            _ffmpeg = ffmpeg;
            _settings = settings;
        }

        public IThumbnailGenerator Get()
        {
            return new FileSystemCachedThumbnailGenerator(
                new FfmpegThumbnailGenerator(
                    _ffmpeg,
                    _settings.TempFileLocation),
                _settings.ThumbnailLocation);
        }
    }
}
