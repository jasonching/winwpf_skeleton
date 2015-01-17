using Caliburn.Micro;
using Castle.Windsor;
using MyApp.Core.Windsor;
using MyApp.Wpf.Launcher;
using MyApp.Wpf.Windsor;
using MyApp.Wpf.Windsor.Bootstrapper;
using System;
using System.Collections.Generic;

namespace MyApp.Wpf.Bootstrapper
{
    public class MainBootstrapper : BootstrapperBase
    {
        // Integrating Caliburn with Castle Windsor
        // https://gist.github.com/bryanhunter/1127914
        private IWindsorContainer container;

        public MainBootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            container = GlobalContainerAccessor.Instance.Container;
            container.Register(
                new MainContainerBootstrapper(),
                new UiContainerBootstrapper());
        }

        protected override object GetInstance(Type service, string key)
        {
            return string.IsNullOrWhiteSpace(key)
                   ? container.Resolve(service)
                   : container.Resolve(key, service);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return (IEnumerable<object>)container.ResolveAll(service);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            DisplayRootViewFor<LauncherViewModel>();
        }
    }
}
