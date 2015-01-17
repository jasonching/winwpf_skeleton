using Caliburn.Micro;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MyApp.Core.Windsor;

namespace MyApp.Wpf.Test.Windsor.Bootstrapper
{
    internal class TestContainerBootstrapper : IWindsorContainerBootstrapper
    {
        public MockObjectsProvider MockObjectsProvider { get; set; }

        public void Register(IWindsorContainer container)
        {
            container.Register(Component.For<IWindowManager>()
                .Instance(MockObjectsProvider.WindowManager.Object));
        }
    }
}
