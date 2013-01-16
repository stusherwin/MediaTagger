using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaTagger.Server
{
    public class ThumbnailGeneratorSettings
    {
        public string FfmpegPath { get; set; }
        public string TempFileLocation { get; set; }
        public string ThumbnailLocation { get; set; }
    }
}