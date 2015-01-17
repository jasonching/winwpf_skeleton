using Caliburn.Micro;
using MyApp.Wpf.GameOfLife;
using MyApp.Wpf.Windsor;

namespace MyApp.Wpf.Launcher
{
    public class LauncherViewModel : PropertyChangedBase
    {
        public IWindowManager WindowManager { get; set; }

        // The command binding is done by convention
        public void LaunchGameOfLife()
        {
            var viewModel = GlobalContainerAccessor.Instance.Container.Resolve<GameOfLifeViewModel>();

            if (viewModel.IsActive)
                return;

            WindowManager.ShowWindow(viewModel);
        }
    }
}
