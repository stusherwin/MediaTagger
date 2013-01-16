using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using MediaTagger.Core;

namespace MediaTagger.Server
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