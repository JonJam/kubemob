using System.Collections.Generic;
using System.Threading.Tasks;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.PersistentVolumeClaims
{
    [Preserve(AllMembers = true)]
    public class PersistentVolumeClaimsViewModel : ObjectListViewModelBase
    {
        public PersistentVolumeClaimsViewModel(
            INavigationService navigationService,
            IKubernetesService kubernetesService,
            IPopupService popupService)
            : base(navigationService, kubernetesService, popupService)
        {
        }

        protected override Task<IList<ObjectSummary>> GetObjectSummaries() =>
            this.KubernetesService.GetPersistentVolumeClaimSummaries();

        protected override Task OnObjectSummarySelectedExecute(object obj)
        {
            ObjectSummary selected = (ObjectSummary)obj;

            return this.NavigationService.NavigateToPersistentVolumeClaimDetailPage(selected.Name, selected.NamespaceName);
        }
    }
}
