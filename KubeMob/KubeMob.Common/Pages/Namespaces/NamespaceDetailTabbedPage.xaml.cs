using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.ViewModels.Events;
using KubeMob.Common.ViewModels.Namespaces;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Pages.Namespaces
{
    public partial class NamespaceDetailTabbedPage : TabbedPage
    {
        [Preserve]
        public NamespaceDetailTabbedPage() => this.InitializeComponent();

        protected override async void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            Page page = this.CurrentPage;

            if (page.BindingContext is EventsViewModel vm)
            {
                NamespaceDetailViewModel detailViewModel = (NamespaceDetailViewModel)this.DetailPage.BindingContext;
                Filter filter = new Filter(detailViewModel.Detail.NamespaceName, other: detailViewModel.Detail.Uid);

                await vm.Initialize(filter);
            }
        }
    }
}