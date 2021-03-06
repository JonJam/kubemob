using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.ViewModels.Conditions;
using KubeMob.Common.ViewModels.Events;
using KubeMob.Common.ViewModels.PersistentVolumeClaims;
using KubeMob.Common.ViewModels.Pods;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Pages.Pods
{
    public partial class PodDetailTabbedPage : TabbedPage
    {
        [Preserve]
        public PodDetailTabbedPage() => this.InitializeComponent();

        protected override async void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            Page page = this.CurrentPage;

            PodDetailViewModel detailViewModel = (PodDetailViewModel)this.DetailPage.BindingContext;

            // detailViewModel.Detail will be null if an error occurs which will have already been handled, so
            // do nothing. Otherwise try load events.
            if (page.BindingContext is EventsViewModel eventsViewModel &&
                detailViewModel.Detail != null)
            {
                Filter filter = new Filter(detailViewModel.Detail.NamespaceName, other: detailViewModel.Detail.Uid);

                await eventsViewModel.Initialize(filter);
            }
            else if (page.BindingContext is PersistentVolumeClaimsViewModel persistentVolumeClaimsViewModel &&
                detailViewModel.Detail != null)
            {
                Filter filter = new Filter(
                    detailViewModel.Detail.NamespaceName,
                    other: string.Join(",", detailViewModel.Detail.PersistentVolumeClaims));

                await persistentVolumeClaimsViewModel.Initialize(filter);
            }
            else if (page.BindingContext is ConditionsViewModel conditionsViewModel &&
                     detailViewModel.Detail != null)
            {
                await conditionsViewModel.Initialize(detailViewModel.Detail.Conditions);
            }
        }
    }
}