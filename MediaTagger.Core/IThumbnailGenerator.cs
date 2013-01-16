using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaTagger.Core
{
    public interface IThumbnailGenerator
    {
        ThumbnailImage Generate(MediaFile videoFile, TimeSpan thumbnailTime);
    }
}
