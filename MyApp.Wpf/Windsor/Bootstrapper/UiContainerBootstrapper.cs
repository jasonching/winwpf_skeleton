using Caliburn.Micro;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MyApp.Core.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Wpf.Windsor.Bootstrapper
{
    public class UiContainerBootstrapper : IWindsorContainerBootstrapper
    {
        public void Register(IWindsorContainer container)
        {
            container.Register(Component.For<IWindowManager, WindowManager>());
        }
    }
}
