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
    public class PersistentVolumeClaimDetailViewModel : ObjectDetailViewModelBase<PersistentVolumeClaimDetail>
    {
        public PersistentVolumeClaimDetailViewModel(
            IKubernetesService kubernetesService,
            IPopupService popupService,
            INavigationService navigationService)
            : base(kubernetesService, popupService, navigationService)
        {
        }

        protected override Task<PersistentVolumeClaimDetail> GetObjectDetail(string name, string namespaceName) => this.KubernetesService.GetPersistentVolumeClaimDetail(name, namespaceName);
    }
}
