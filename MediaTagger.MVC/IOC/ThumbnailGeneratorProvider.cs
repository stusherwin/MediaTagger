using MediaTagger.Core;
using MediaTagger.Core.Thumbnails;
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
                new FileSystemImageCache(_settings.ThumbnailBaseLocation),
                new FileSystemImageCache(_settings.ThumbnailLocation));
        }
    }
}
