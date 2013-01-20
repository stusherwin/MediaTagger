using MediaTagger.Core;

namespace MediaTagger.Mvc
{
    public class ThumbnailInputModel
    {
        public int FileId { get; set; }
        public Duration ThumbnailDuration { get; set; }
    }
}