using Castle.MicroKernel.Facilities;
using Common.Logging;

namespace MyApp.Core.Windsor.Facility
{
    public class LifeCycleTracerFacility : AbstractFacility
    {
        private static readonly ILog logger = LogManager.GetLogger<LifeCycleTracerFacility>();

        protected override void Init()
        {
            Kernel.ComponentCreated += Kernel_ComponentCreated;

            // This will only be fired for IDispoable components
            Kernel.ComponentDestroyed += Kernel_ComponentDestroyed;
        }

        void Kernel_ComponentDestroyed(global::Castle.Core.ComponentModel model, object instance)
        {
            logger.DebugFormat("Component Destroyed: {0}, {1}", instance.GetType(), instance);
        }

        void Kernel_ComponentCreated(global::Castle.Core.ComponentModel model, object instance)
        {
            logger.DebugFormat("Component Created: {0}", instance.GetType());
        }
    }
}
