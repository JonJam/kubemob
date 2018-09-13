using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.ViewModels.Endpoints;
using KubeMob.Common.ViewModels.Events;
using KubeMob.Common.ViewModels.Pods;
using KubeMob.Common.ViewModels.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Pages.Services
{
    public partial class ServiceDetailTabbedPage : TabbedPage
    {
        [Preserve]
        public ServiceDetailTabbedPage() => this.InitializeComponent();

        protected override async void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            Page page = this.CurrentPage;

            ServiceDetailViewModel detailViewModel = (ServiceDetailViewModel)this.DetailPage.BindingContext;

            // detailViewModel.Detail will be null if an error occurs which will have already been handled, so
            // do nothing. Otherwise try load events.
            if (page.BindingContext is EventsViewModel eventsViewModel &&
                detailViewModel.Detail != null)
            {
                Filter filter = new Filter(detailViewModel.Detail.NamespaceName, other: detailViewModel.Detail.Uid);

                await eventsViewModel.Initialize(filter);
            }
            else if (page.BindingContext is PodsViewModel podsViewModel &&
                detailViewModel.Detail != null)
            {
                Filter filter = new Filter(
                    detailViewModel.Detail.NamespaceName,
                    labelSelector: detailViewModel.Detail.Selector);

                await podsViewModel.Initialize(filter);
            }
            else if (page.BindingContext is EndpointDetailViewModel endpointDetailViewModel &&
                     detailViewModel.Detail != null)
            {
                ObjectId objectId = new ObjectId(detailViewModel.Detail.Name, detailViewModel.Detail.NamespaceName);

                await endpointDetailViewModel.Initialize(objectId);
            }
        }
    }
}