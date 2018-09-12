using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.ViewModels.Conditions;
using KubeMob.Common.ViewModels.Events;
using KubeMob.Common.ViewModels.HorizontalPodAutoscalers;
using KubeMob.Common.ViewModels.PersistentVolumeClaims;
using KubeMob.Common.ViewModels.Pods;
using KubeMob.Common.ViewModels.ReplicaSets;
using KubeMob.Common.ViewModels.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Pages.ReplicaSets
{
    public partial class ReplicaSetDetailTabbedPage : TabbedPage
    {
        [Preserve]
        public ReplicaSetDetailTabbedPage() => this.InitializeComponent();

        // TODO Tabs
        protected override async void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            Page page = this.CurrentPage;

            ReplicaSetDetailViewModel detailViewModel = (ReplicaSetDetailViewModel)this.DetailPage.BindingContext;

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
            else if (page.BindingContext is ServicesViewModel servicesViewModel &&
                     detailViewModel.Detail != null)
            {
                Filter filter = new Filter(
                    detailViewModel.Detail.NamespaceName,
                    other: detailViewModel.Detail.RelatedSelector);

                await servicesViewModel.Initialize(filter);
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