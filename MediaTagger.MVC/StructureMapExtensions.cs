using MediaTagger.Mvc.IOC;
using StructureMap.Configuration.DSL.Expressions;
using StructureMap;

namespace MediaTagger.Mvc
{
    public static class StructureMapExtensions
    {
        public static ProviderContext<T> FindAndUseProvider<T>(this CreatePluginFamilyExpression<T> instance, Container container)
        {
            return new ProviderContext<T>(instance, container);
        }

        public class ProviderContext<T>
        {
            readonly CreatePluginFamilyExpression<T> _instance;
            readonly Container _container;

            public ProviderContext(CreatePluginFamilyExpression<T> instance, Container container)
            {
                _instance = instance;
                _container = container;
            }

            public void OfType<TProvider>()
                where TProvider : IProvider<T>
            {
                _instance.Use(() => _container.GetInstance<TProvider>().Get());
            }
        }
    }
}