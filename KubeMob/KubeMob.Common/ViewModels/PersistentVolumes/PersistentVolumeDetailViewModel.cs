using System.Threading.Tasks;
using System.Windows.Input;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.PersistentVolumes
{
    [Preserve(AllMembers = true)]
    public class PersistentVolumeDetailViewModel : ObjectDetailViewModelBase<PersistentVolumeDetail>
    {
        // TODO Refactor commands.
        public PersistentVolumeDetailViewModel(
            IKubernetesService kubernetesService,
            IPopupService popupService,
            INavigationService navigationService)
            : base(kubernetesService, popupService, navigationService)
        {
            this.NavigateToPersistentVolumeClaimCommand = new Command(async () => await this.OnNavigateToPersistentVolumeClaimCommandExecute());
            this.NavigateToStorageClassCommand = new Command(async () => await this.OnNavigateToStorageClassCommandExecute());
        }

        public ICommand NavigateToPersistentVolumeClaimCommand
        {
            get;
        }

        public ICommand NavigateToStorageClassCommand
        {
            get;
        }

        protected override Task<PersistentVolumeDetail> GetObjectDetail(string name, string namespaceName) => this.KubernetesService.GetPersistentVolumeDetail(name);

        private Task OnNavigateToPersistentVolumeClaimCommandExecute() => this.NavigationService.NavigateToPersistentVolumeClaimDetailPage(
            this.Detail.Claim.Name,
            this.Detail.Claim.NamespaceName);

        private Task OnNavigateToStorageClassCommandExecute() => this.NavigationService.NavigateToStorageClassDetailPage(
            this.Detail.StorageClass);
    }
}
