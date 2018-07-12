using KubeMob.Common.Pages.Base;
using KubeMob.Common.ViewModels.Base;
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

            var page = this.CurrentPage;

            // TODO Wire up to events viewmodel and handle already being loaded.
            //if (page.BindingContext is NamespaceDetailViewModel vm)
            //{
            //    var objectId = ((NamespaceDetailTabbedViewModel)this.BindingContext).ObjectId;
            //    //await vm.Initialize(objectId);
            //    var a = 1;
            //}
        }
    }
}