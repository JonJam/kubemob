using System.Threading.Tasks;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.StorageClasses
{
    [Preserve(AllMembers = true)]
    public class StorageClassDetailViewModel : ObjectDetailViewModelBase<StorageClassDetail>
    {
        public StorageClassDetailViewModel(
            IKubernetesService kubernetesService,
            IPopupService popupService,
            INavigationService navigationService)
            : base(kubernetesService, popupService, navigationService)
        {
        }

        protected override Task<StorageClassDetail> GetObjectDetail(string name, string namespaceName) => this.KubernetesService.GetStorageClassDetail(name);
    }
}
