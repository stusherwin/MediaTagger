using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaTagger.Server
{
    public class ThumbnailInputModel
    {
        public int FileId { get; set; }
        public TimeSpan ThumbnailTime { get; set; }
    }
}