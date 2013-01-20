using MediaTagger.Core;

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