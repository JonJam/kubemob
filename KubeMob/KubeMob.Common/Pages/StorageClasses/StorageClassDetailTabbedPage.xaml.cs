using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.ViewModels.PersistentVolumes;
using KubeMob.Common.ViewModels.StorageClasses;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Pages.StorageClasses
{
    public partial class StorageClassDetailTabbedPage : TabbedPage
    {
        [Preserve]
        public StorageClassDetailTabbedPage() => this.InitializeComponent();

        protected override async void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            Page page = this.CurrentPage;

            StorageClassDetailViewModel detailViewModel = (StorageClassDetailViewModel)this.DetailPage.BindingContext;

            // detailViewModel.Detail will be null if an error occurs which will have already been handled, so
            // do nothing. Otherwise try load events.
            if (page.BindingContext is PersistentVolumesViewModel persistentVolumesViewModel &&
                detailViewModel.Detail != null)
            {
                Filter filter = new Filter(other: detailViewModel.Detail.Name);

                await persistentVolumesViewModel.Initialize(filter);
            }
        }
    }
}