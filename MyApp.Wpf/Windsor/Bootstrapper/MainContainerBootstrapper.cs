using Caliburn.Micro;
using Castle.Core;
using Castle.Facilities.EventWiring;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MyApp.Core.Windsor;
using MyApp.Core.Windsor.Facility;
using MyApp.Wpf.GameOfLife.GameBoard;
using MyApp.Wpf.GameOfLife.GameControlPanel;
using System;

namespace MyApp.Wpf.Windsor.Bootstrapper
{
    public class MainContainerBootstrapper : IWindsorContainerBootstrapper
    {
        public static readonly String ViewModelConvention = "ViewModel";

        public void Register(IWindsorContainer container)
        {
            container.AddFacility<TypedFactoryFacility>();
            container.AddFacility<LifeCycleTracerFacility>();
            container.AddFacility<ViewModelReleaseFacility>();
            container.AddFacility<EventWiringFacility>();

            // All ViewModels
            container.Register(Types.FromThisAssembly()
                .Where(x => x.Name.EndsWith(ViewModelConvention) && x.Name != "GameControlPanelViewModel"));

            container.Register(Component.For<GameControlPanelViewModel>()
                .PublishEvent(g => g.StartEvent += null, x => x.To<GameBoardViewModel>(l => l.Start(null, null)))
                .PublishEvent(g => g.ResetEvent += null, x => x.To<GameBoardViewModel>(l => l.Reset(null, System.Drawing.Size.Empty)))
                .PublishEvent(g => g.StopEvent += null, x => x.To<GameBoardViewModel>(l => l.Stop(null, null))));
        }
    }
}
