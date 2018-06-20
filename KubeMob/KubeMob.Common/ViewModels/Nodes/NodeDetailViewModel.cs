using System.Threading.Tasks;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.Nodes
{
    [Preserve(AllMembers = true)]
    public class NodeDetailViewModel : ObjectDetailViewModelBase<NodeDetail>
    {
        public NodeDetailViewModel(
            IKubernetesService kubernetesService,
            IPopupService popupService,
            INavigationService navigationService)
            : base(kubernetesService, popupService, navigationService)
        {
        }

        protected override Task<NodeDetail> GetObjectDetail(string name, string namespaceName) => this.KubernetesService.GetNodeDetail(name);

        protected override async Task GetRelatedObjects(string name, string namespaceName)
        {
            await this.KubernetesService.Test();
        }
    }
}
