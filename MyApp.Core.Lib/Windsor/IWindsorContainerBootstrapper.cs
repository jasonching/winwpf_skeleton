using Castle.Windsor;

namespace MyApp.Core.Windsor
{
    public interface IWindsorContainerBootstrapper
    {
        void Register(IWindsorContainer container);
    }
}
