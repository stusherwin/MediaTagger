using FubuMVC.Core;

namespace MediaTagger.Mvc
{
    public class ConfigureFubuMVC : FubuRegistry
    {
        public ConfigureFubuMVC()
        {
            // This line turns on the basic diagnostics and request tracing
            IncludeDiagnostics(true);

            // All public methods from concrete classes ending in "Controller"
            // in this assembly are assumed to be action methods
            Actions.IncludeClassesSuffixedWithController();

            // Policies
            Routes
                .IgnoreControllerNamesEntirely()
                .IgnoreMethodSuffix("Html")
                //.HomeIs<HomeController>(c => c.GetHtml())
                .RootAtAssemblyNamespace();

            // Match views to action methods by matching
            // on model type, view name, and namespace
            Views.TryToAttachWithDefaultConventions();

            Output.ToJson.WhenTheOutputModelIs<JsonMessage>();
            Output.ToHtml.WhenTheOutputModelIs<string>();
            Output.ToBehavior<ThumbnailBehaviour>().WhenTheOutputModelIs<ThumbnailOutputModel>();
            Output.ToBehavior<MediaBehaviour>().WhenTheOutputModelIs<MediaOutputModel>();

            Models.ConvertUsing<DurationConverter>();
        }
    }
}