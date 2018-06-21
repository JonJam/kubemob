using System.Threading.Tasks;
using System.Windows.Input;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
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
            : base(kubernetesService, popupService, navigationService) => this.ViewRelatedPersistentVolumesCommand = new Command(async () => await this.OnViewRelatedPersistentVolumesCommand());

        public ICommand ViewRelatedPersistentVolumesCommand
        {
            get;
        }

        protected override Task<StorageClassDetail> GetObjectDetail(string name, string namespaceName) => this.KubernetesService.GetStorageClassDetail(name);

        private Task OnViewRelatedPersistentVolumesCommand() => this.NavigationService.NavigateToPersistentVolumesPage(this.Name);
    }
}
