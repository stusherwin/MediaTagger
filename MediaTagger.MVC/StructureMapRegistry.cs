using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap;
using FubuMVC.StructureMap;
using FubuCore.Configuration;
using MediaTagger.Core;
using MediaTagger.Core.Xml;
using System.Web.Hosting;

namespace MediaTagger.Server
{
    public class StructureMapRegistry
    {
        public static Container CreateContainer()
        {
            var container = new Container();

            container.Configure(x =>
            {
                x.Scan(s =>
                {
                    s.TheCallingAssembly();
                    s.Convention<SettingsScanner>();
                });

                x.For<ISettingsProvider>().Use<AppSettingsProvider>();

                x.For<Library>().Singleton().Use(() =>
                {
                    var converter = container.GetInstance<LibraryXmlConverter>();
                    var settings = container.GetInstance<LibrarySettings>();

                    return converter.ReadFromFile(settings.LibraryFile);
                });

                x.For<FfmpegWrapper>().Use(() =>
                {
                    var settings = container.GetInstance<ThumbnailGeneratorSettings>();

                    return new FfmpegWrapper(
                        HostingEnvironment.MapPath(settings.FfmpegPath)
                    );
                });

                x.For<IThumbnailGenerator>().Use(() =>
                {
                    var settings = container.GetInstance<ThumbnailGeneratorSettings>();
                    var ffmpeg = container.GetInstance<FfmpegWrapper>();

                    return new FileSystemCachedThumbnailGenerator(
                        new FfmpegThumbnailGenerator(
                            ffmpeg, 
                            settings.TempFileLocation),
                        settings.ThumbnailLocation);
                });
            });

            return container;
        }
    }
}