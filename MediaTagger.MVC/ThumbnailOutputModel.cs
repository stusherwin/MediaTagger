using MediaTagger.Core;
using MediaTagger.Core.Thumbnails;

namespace MediaTagger.Mvc
{
    public class ThumbnailOutputModel
    {
        public ThumbnailImage Thumbnail;

        public ThumbnailOutputModel(ThumbnailImage thumbnail)
        {
            Thumbnail = thumbnail;
        }
    }
}