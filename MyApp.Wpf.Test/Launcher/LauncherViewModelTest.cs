using Moq;
using MyApp.Wpf.GameOfLife;
using MyApp.Wpf.Launcher;
using MyApp.Wpf.Test.Nunit;
using NUnit.Framework;
using System.Collections.Generic;

namespace MyApp.Wpf.Test.Launcher
{
    [TestFixture]
    public class LauncherViewModelTest : AbstractTest
    {
        [Test]
        public void LaunchGameOfLifeTest()
        {
            // Given
            var launcherViewModel = WindsorContainer.Resolve<LauncherViewModel>();
            var windowManagerMock = MockObjectsProvider.WindowManager;

            // When
            launcherViewModel.LaunchGameOfLife();

            // Then
            windowManagerMock.Verify(
                v => v.ShowWindow(It.IsAny<GameOfLifeViewModel>(), It.IsAny<object>(), It.IsAny<IDictionary<string, object>>()),
                Times.Once,
                "Not invoking ShowWindow to launch GameOfLive View");
        }
    }
}
