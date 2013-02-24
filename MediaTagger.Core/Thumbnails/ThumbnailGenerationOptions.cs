using System;

namespace MediaTagger.Core.Thumbnails
{
    public class ThumbnailGenerationOptions
    {
        public Duration PointInTime { get; private set; }
        public ImageSize ImageSize { get; private set; }

        public ThumbnailGenerationOptions(Duration pointInTime, ImageSize imageSize)
        {
            if (pointInTime == null)
                throw new ArgumentNullException("pointInTime");

            if (imageSize == null)
                throw new ArgumentNullException("imageSize");

            PointInTime = pointInTime;
            ImageSize = imageSize;
        }
    }
}
