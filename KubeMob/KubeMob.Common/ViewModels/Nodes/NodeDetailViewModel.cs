using System.Threading.Tasks;
using System.Windows.Input;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
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

        public ICommand ViewRelatedPodsCommand => new Command(async () => await this.OnViewRelatedPodsCommandExecute());

        protected override Task<NodeDetail> GetObjectDetail(string name, string namespaceName) => this.KubernetesService.GetNodeDetail(name);

        private Task OnViewRelatedPodsCommandExecute() => this.NavigationService.NavigateToPodsPage($"spec.nodeName={this.Name}");
    }
}
