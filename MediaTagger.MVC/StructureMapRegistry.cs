using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap;
using FubuMVC.StructureMap;
using FubuCore.Configuration;

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
            });

            return container;
        }
    }
}