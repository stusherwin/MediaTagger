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
        private ThumbnailGeneratorSettings _settings;

        public ThumbnailController(Library library, IThumbnailGenerator generator, ThumbnailGeneratorSettings settings)
        {
            _library = library;
            _generator = generator;
            _settings = settings;
        }

        public ThumbnailOutputModel get_Thumbnail_FileId(ThumbnailInputModel model)
        {
            var videoFile = _library.Files.FirstOrDefault(f => f.Id == model.FileId);

            if (videoFile == null)
                throw new HttpException(404, "Not found");

            TimeSpan thumbnailTime = GetDefaultThumbnailTime(videoFile);

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

        //TODO: move to Duration value object
        private TimeSpan GetDefaultThumbnailTime(MediaFile videoFile)
        {
            return new TimeSpan((long)(videoFile.Duration.Ticks * _settings.DefaultThumbnailTimePercentage / 100.0));
        }
    }
}