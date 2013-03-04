using MediaTagger.Core;
using MediaTagger.Core.FileSystem;
using MediaTagger.Core.Thumbnails;
using MediaTagger.Mvc.Configuration;

namespace MediaTagger.Mvc.IOC
{
    public class ThumbnailGeneratorProvider : IProvider<IThumbnailGenerator>
    {
        readonly FfmpegWrapper _ffmpeg;
        readonly FileManager _fileManager;
        readonly ThumbnailGeneratorSettings _settings;

        public ThumbnailGeneratorProvider(FfmpegWrapper ffmpeg, FileManager fileManager, ThumbnailGeneratorSettings settings)
        {
            _ffmpeg = ffmpeg;
            _fileManager = fileManager;
            _settings = settings;
        }

        public IThumbnailGenerator Get()
        {
            return new SizedThumbnailGenerator(
                _fileManager, 
                _settings.ThumbnailLocation, 
                new BaseThumbnailGenerator(
                    _fileManager, 
                    _settings.ThumbnailBaseLocation, 
                    new FfmpegThumbnailGenerator(
                        _ffmpeg,
                        _fileManager,
                        _settings.TempFileLocation)));
        }
    }
}
