using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.ViewModels.Events;
using KubeMob.Common.ViewModels.Namespaces;
using KubeMob.Common.ViewModels.Nodes;
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

            if (page.BindingContext is EventsViewModel vm &&
                detailViewModel.Detail != null)
            {
                // detailViewModel.Detail will be null if an error occurs which will have already been handled, so
                // do nothing. Otherwise try load events.
                Filter filter = new Filter(detailViewModel.Detail.NamespaceName, other: detailViewModel.Detail.Uid);

                await vm.Initialize(filter);
            }


            //TODO handle other tabs.
        }
    }
}