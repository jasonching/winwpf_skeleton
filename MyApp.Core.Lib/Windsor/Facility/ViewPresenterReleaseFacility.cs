using Castle.Core;
using Castle.MicroKernel.Facilities;
using Common.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MyApp.Core.Windsor.Facility
{
    public class ViewPresenterReleaseFacility : AbstractFacility
    {
        private static readonly ILog logger = LogManager.GetLogger<ViewPresenterReleaseFacility>();

        private List<ViewPresenter> viewPresenters = new List<ViewPresenter>();

        protected override void Init()
        {
            Kernel.ComponentCreated += Kernel_ComponentCreated;
        }

        private void Kernel_ComponentCreated(ComponentModel model, object instance)
        {
            var presenter = instance as IPresenter;
            if (presenter == null)
                return;

            var viewPropertyInfo = model.Properties
                .Select(p => p.Property)
                .FirstOrDefault(p => typeof(IView).IsAssignableFrom(p.PropertyType));

            if (viewPropertyInfo == null)
                return;

            var view = viewPropertyInfo.GetValue(instance) as IComponent;

            if (view == null)
                return;

            viewPresenters.Add(new ViewPresenter { View = view, Presenter = presenter });
            view.Disposed += form_Disposed;

            logger.DebugFormat("View and presenter found: {0}, {1}", view, presenter);            
        }

        private void form_Disposed(object sender, EventArgs e)
        {
            var matchedFormPresenters = viewPresenters.Where(fp => fp.View.Equals(sender)).ToList();

            foreach (var matchedFormPresenter in matchedFormPresenters)
            {
                matchedFormPresenter.View.Disposed -= form_Disposed;

                // doesn't work.  need to release it from the factory
                // how to find out which factory it belongs to?
                // can't figure out.  it's working when I am not using factory

                logger.DebugFormat("Releasing View and presenter: {0}, {1}", matchedFormPresenter.View, matchedFormPresenter.Presenter);

                Kernel.ReleaseComponent(matchedFormPresenter.Presenter);
                Kernel.ReleaseComponent(matchedFormPresenter.View);

                viewPresenters.Remove(matchedFormPresenter);
            }
        }
    }
}
