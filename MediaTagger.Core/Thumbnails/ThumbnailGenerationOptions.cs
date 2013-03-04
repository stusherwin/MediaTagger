using System;

namespace MediaTagger.Core.Thumbnails
{
    public class ThumbnailGenerationOptions
    {
        public PointInTime PointInTime { get; private set; }
        public ImageType ImageType { get; private set; }
        public ImageSize ImageSize { get; private set; }

        public ThumbnailGenerationOptions(PointInTime pointInTime, ImageType imageType, ImageSize imageSize)
        {
            if (pointInTime == null)
                throw new ArgumentNullException("pointInTime");

            if (imageType == null)
                throw new ArgumentNullException("imageType");

            if (imageSize == null)
                throw new ArgumentNullException("imageSize");

            PointInTime = pointInTime;
            ImageType = imageType;
            ImageSize = imageSize;
        }
    }
}
