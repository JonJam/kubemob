using System.Threading.Tasks;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.ReplicaSets
{
    [Preserve(AllMembers = true)]
    public class ReplicaSetDetailViewModel : ObjectDetailViewModelBase<ReplicaSetDetail>
    {
        public ReplicaSetDetailViewModel(
            IKubernetesService kubernetesService,
            IPopupService popupService)
            : base(kubernetesService, popupService)
        {
        }

        protected override Task<ReplicaSetDetail> GetObjectDetail(string name, string namespaceName) => this.KubernetesService.GetReplicaSetDetail(name, namespaceName);
    }
}
