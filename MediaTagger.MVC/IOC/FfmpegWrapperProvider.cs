using MediaTagger.Core;
using System.Web.Hosting;
using MediaTagger.Mvc.Configuration;

namespace MediaTagger.Mvc.IOC
{
    public class FfmpegWrapperProvider : IProvider<FfmpegWrapper>
    {
        readonly ThumbnailGeneratorSettings _settings;

        public FfmpegWrapperProvider(ThumbnailGeneratorSettings settings)
        {
            _settings = settings;
        }

        public FfmpegWrapper Get()
        {
            return new FfmpegWrapper(_settings.FfmpegPath);
        }
    }
}
