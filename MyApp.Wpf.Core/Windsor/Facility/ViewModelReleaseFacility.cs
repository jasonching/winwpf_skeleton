using Caliburn.Micro;
using Castle.Core;
using Castle.MicroKernel.Facilities;

namespace MyApp.Core.Windsor.Facility
{
    public class ViewModelReleaseFacility : AbstractFacility
    {
        private static readonly Common.Logging.ILog logger = Common.Logging.LogManager.GetLogger<ViewModelReleaseFacility>();

        protected override void Init()
        {
            Kernel.ComponentCreated += Kernel_ComponentCreated;
        }

        void Kernel_ComponentCreated(ComponentModel model, object instance)
        {
            // More info about IScreen:
            // https://caliburnmicro.codeplex.com/wikipage?title=Screens%2c%20Conductors%20and%20Composition

            // no need to do this for Singleton
            if (model.LifestyleType != LifestyleType.Transient)
                return;

            var screen = instance as IScreen;

            if (screen == null)
                return;

            screen.Deactivated += ScreenDeactivated;
        }

        private void ScreenDeactivated(object sender, DeactivationEventArgs e)
        {
            if (!e.WasClosed)
                return;

            var screen = sender as IScreen;

            if (screen == null)
                return;

            Release(screen);
        }

        private void Release(IScreen screen)
        {
            logger.DebugFormat("Releasing Object: {0}", screen);

            screen.Deactivated -= ScreenDeactivated;
            Kernel.ReleaseComponent(screen);
        }
    }
}
