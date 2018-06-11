using System.Threading.Tasks;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.Deployments
{
    [Preserve(AllMembers = true)]
    public class DeploymentDetailViewModel : ObjectDetailViewModelBase<DeploymentDetail>
    {
        public DeploymentDetailViewModel(
            IKubernetesService kubernetesService,
            IPopupService popupService)
            : base(kubernetesService, popupService)
        {
        }

        protected override Task<DeploymentDetail> GetObjectDetail(string name, string namespaceName) => this.KubernetesService.GetDeploymentDetail(name, namespaceName);
    }
}
