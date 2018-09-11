using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.ViewModels.Events;
using KubeMob.Common.ViewModels.Jobs;
using KubeMob.Common.ViewModels.Pods;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Pages.Jobs
{
    public partial class JobDetailTabbedPage : TabbedPage
    {
        [Preserve]
        public JobDetailTabbedPage() => this.InitializeComponent();

        protected override async void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            Page page = this.CurrentPage;

            JobDetailViewModel detailViewModel = (JobDetailViewModel)this.DetailPage.BindingContext;

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
                    other: detailViewModel.Detail.Uid);

                await podsViewModel.Initialize(filter);
            }
        }
    }
}