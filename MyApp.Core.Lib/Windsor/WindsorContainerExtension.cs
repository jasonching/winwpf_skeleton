using Castle.Windsor;

namespace MyApp.Core.Windsor
{
    public static class WindsorContainerExtension
    {
        public static void Register(this IWindsorContainer container, params IWindsorContainerBootstrapper[] bootstrappers)
        {
            foreach (var bootstrapper in bootstrappers)
                bootstrapper.Register(container);
        }
    }
}
