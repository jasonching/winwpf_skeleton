using Castle.Windsor;

namespace MyApp.Wpf.Windsor
{
    public class GlobalContainerAccessor : IContainerAccessor
    {
        #region Singleton
        private static readonly object instanceLock = new object();
        private static GlobalContainerAccessor instance;

        public static GlobalContainerAccessor Instance
        {
            get
            {
                if (instance == null)
                {
                    lock(instanceLock)
                    {
                        if (instance == null)
                            instance = new GlobalContainerAccessor();
                    }
                }

                return instance;
            }
        }
        #endregion

        #region Container
        private readonly object containerLock = new object();
        private IWindsorContainer container;

        public IWindsorContainer Container
        {
            get
            {
                if (container == null)
                {
                    lock (containerLock)
                    {
                        if (container == null)
                            container = new WindsorContainer();
                    }
                }

                return container;
            }
        }
        #endregion

        private GlobalContainerAccessor() { }

        public void Release()
        {
            if (container == null)
                return;

            lock (containerLock)
            {
                if (container == null)
                    return;

                container.Dispose();
                container = null;
            }
        }
        
    }
}
