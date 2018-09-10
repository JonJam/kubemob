using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.ViewModels.Deployments;
using KubeMob.Common.ViewModels.Events;
using KubeMob.Common.ViewModels.HorizontalPodAutoscalers;
using KubeMob.Common.ViewModels.ReplicaSets;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Pages.Deployments
{
    public partial class DeploymentDetailTabbedPage : TabbedPage
    {
        [Preserve]
        public DeploymentDetailTabbedPage() => this.InitializeComponent();

        protected override async void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            Page page = this.CurrentPage;

            DeploymentDetailViewModel detailViewModel = (DeploymentDetailViewModel)this.DetailPage.BindingContext;

            // detailViewModel.Detail will be null if an error occurs which will have already been handled, so
            // do nothing. Otherwise try load events.
            if (page.BindingContext is EventsViewModel eventsViewModel &&
                detailViewModel.Detail != null)
            {
                Filter filter = new Filter(detailViewModel.Detail.NamespaceName, other: detailViewModel.Detail.Uid);

                await eventsViewModel.Initialize(filter);
            }
            else if (page.BindingContext is ReplicaSetsViewModel replicaSetsViewModel &&
                     detailViewModel.Detail != null)
            {
                Filter filter = new Filter(
                    detailViewModel.Detail.NamespaceName,
                    other: detailViewModel.Detail.Uid);

                await replicaSetsViewModel.Initialize(filter);
            }
            else if (page.BindingContext is HorizontalPodAutoscalersViewModel horizontalPodAutoscalersViewModel &&
                     detailViewModel.Detail != null)
            {
                Filter filter = new Filter(
                    detailViewModel.Detail.NamespaceName,
                    other: detailViewModel.Detail.Name);

                await horizontalPodAutoscalersViewModel.Initialize(filter);
            }
        }
    }
}