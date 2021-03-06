using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.ViewModels.Conditions;
using KubeMob.Common.ViewModels.Events;
using KubeMob.Common.ViewModels.Nodes;
using KubeMob.Common.ViewModels.Pods;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Pages.Nodes
{
    public partial class NodeDetailTabbedPage : TabbedPage
    {
        [Preserve]
        public NodeDetailTabbedPage() => this.InitializeComponent();

        protected override async void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            Page page = this.CurrentPage;

            NodeDetailViewModel detailViewModel = (NodeDetailViewModel)this.DetailPage.BindingContext;

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
                Filter filter = new Filter(KubernetesServiceBase.AllNamespace, $"spec.nodeName={detailViewModel.Detail.Name}");

                await podsViewModel.Initialize(filter);
            }
            else if (page.BindingContext is ConditionsViewModel conditionsViewModel &&
                     detailViewModel.Detail != null)
            {
                await conditionsViewModel.Initialize(detailViewModel.Detail.Conditions);
            }
        }
    }
}