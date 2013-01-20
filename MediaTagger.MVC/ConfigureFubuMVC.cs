using FubuMVC.Core;

namespace MediaTagger.Mvc
{
    public class ConfigureFubuMVC : FubuRegistry
    {
        public ConfigureFubuMVC()
        {
            // This line turns on the basic diagnostics and request tracing
            //IncludeDiagnostics(true);

            // All public methods from concrete classes ending in "Controller"
            // in this assembly are assumed to be action methods
            Actions.IncludeClassesSuffixedWithController();

            // Policies
            Routes
                .IgnoreControllerNamesEntirely()
                .IgnoreMethodSuffix("Html")
                .RootAtAssemblyNamespace();

            Routes.HomeIs<HomeController>(h => h.FrontPage())
                .IgnoreControllerNamespaceEntirely();

            //this.UseSpark();

            // Match views to action methods by matching
            // on model type, view name, and namespace
            //Views.TryToAttachWithDefaultConventions();
            
            //Output.ToJson.WhenTheOutputModelIs<JsonMessage>();
            //Output.ToBehavior<ThumbnailBehaviour>().WhenTheOutputModelIs<ThumbnailOutputModel>();

            Models.ConvertUsing<DurationConverter>();
        }
    }
}