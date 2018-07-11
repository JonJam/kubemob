using KubeMob.Common.Pages.Base;
using KubeMob.Common.ViewModels.Events;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Pages.Namespaces
{
    public partial class NamespaceDetailTabbedPage : TabbedPage
    {
        [Preserve]
        public NamespaceDetailTabbedPage()
        {

            this.InitializeComponent();
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            // TODO On first load of page this is called.

            var page = this.CurrentPage;

            var vm = page.BindingContext;

            // TODO call initialize on VM.
            //if (vm is EventsViewModel e)
            //{
            //    e.Initialize(e.);
            //}
        }
    }
}