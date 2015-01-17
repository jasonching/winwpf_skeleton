using System.ComponentModel;

namespace MyApp.Core.Windsor.Facility
{
    internal class ViewPresenter
    {
        public IComponent View { get; set; }
        public IPresenter Presenter { get; set; }
    }
}
