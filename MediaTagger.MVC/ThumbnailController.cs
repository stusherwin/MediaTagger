using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using FubuMVC.Core.Behaviors;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Web.Hosting;
using MediaTagger.Core;
using MediaTagger.Core.Xml;

namespace MediaTagger.Server
{
    public class ThumbnailController
    {
        private Library _library;
        private IThumbnailGenerator _generator;

        public ThumbnailController(Library library, IThumbnailGenerator generator)
        {
            _library = library;
            _generator = generator;
        }

        public ThumbnailOutputModel get_Thumbnail_FileId(ThumbnailInputModel model)
        {
            var videoFile = _library.Files.FirstOrDefault(f => f.Id == model.FileId);

            if (videoFile == null)
                throw new HttpException(404, "Not found");

            TimeSpan thumbnailTime = new TimeSpan(videoFile.Duration.Ticks / 4);

            return GenerateOutput(videoFile, thumbnailTime);
        }

        public ThumbnailOutputModel get_Thumbnail_FileId_ThumbnailTime(ThumbnailInputModel model)
        {
            var videoFile = _library.Files.FirstOrDefault(f => f.Id == model.FileId);

            if (videoFile == null)
                throw new HttpException(404, "Not found");

            return GenerateOutput(videoFile, model.ThumbnailTime);
        }

        private ThumbnailOutputModel GenerateOutput(MediaFile videoFile, TimeSpan thumbnailTime)
        {
            var thumbnail = _generator.Generate(videoFile, thumbnailTime);

            return new ThumbnailOutputModel(thumbnail);
        }
    }
}