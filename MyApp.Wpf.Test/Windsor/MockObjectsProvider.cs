using Caliburn.Micro;
using Moq;

namespace MyApp.Wpf.Test.Windsor
{
    public class MockObjectsProvider
    {
        public Mock<IWindowManager> WindowManager { get; private set; }

        public MockObjectsProvider()
        {
            WindowManager = new Mock<IWindowManager>();
        }
    }
}
