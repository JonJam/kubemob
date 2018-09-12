using System.Threading.Tasks;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.ReplicationControllers
{
    [Preserve(AllMembers = true)]
    public class ReplicationControllerDetailViewModel : ObjectDetailViewModelBase<ReplicationControllerDetail>
    {
        public ReplicationControllerDetailViewModel(
            IKubernetesService kubernetesService,
            IPopupService popupService,
            INavigationService navigationService)
            : base(kubernetesService, popupService, navigationService)
        {
        }

        protected override Task<ReplicationControllerDetail> GetObjectDetail(string name, string namespaceName) => this.KubernetesService.GetReplicationControllerDetail(name, namespaceName);
    }
}
