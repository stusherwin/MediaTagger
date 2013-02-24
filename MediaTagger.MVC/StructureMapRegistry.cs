using MediaTagger.Core.Thumbnails;
using MediaTagger.Mvc.IOC;
using StructureMap;
using FubuMVC.StructureMap;
using FubuCore.Configuration;
using MediaTagger.Core;

namespace MediaTagger.Mvc
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

                x.For<Library>().Singleton()
                    .FindAndUseProvider(container).OfType<LibraryProvider>();

                x.For<FfmpegWrapper>()
                    .FindAndUseProvider(container).OfType<FfmpegWrapperProvider>();

                x.For<IThumbnailGenerator>()
                    .FindAndUseProvider(container).OfType<ThumbnailGeneratorProvider>();
            });

            return container;
        }
    }
}