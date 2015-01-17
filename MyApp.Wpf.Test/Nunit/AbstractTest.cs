using Castle.Windsor;
using MyApp.Core.Windsor;
using MyApp.Wpf.Test.Windsor;
using MyApp.Wpf.Test.Windsor.Bootstrapper;
using MyApp.Wpf.Windsor;
using MyApp.Wpf.Windsor.Bootstrapper;
using NUnit.Framework;

namespace MyApp.Wpf.Test.Nunit
{
    public abstract class AbstractTest
    {
        public MockObjectsProvider MockObjectsProvider { get; private set; }
        public IWindsorContainer WindsorContainer
        {
            get { return GlobalContainerAccessor.Instance.Container; }
        }

        [SetUp]
        public void Init()
        {
            MockObjectsProvider = new MockObjectsProvider();

            WindsorContainer.Register(
                   new MainContainerBootstrapper(),
                   new TestContainerBootstrapper { MockObjectsProvider = MockObjectsProvider });
        }

        [TearDown]
        public void CleanUp()
        {
            GlobalContainerAccessor.Instance.Release();
            
            MockObjectsProvider = null;
        }
    }
}
