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
            this.ViewRelatedPodsCommand = new Command(async () => await this.OnViewRelatedPodsCommandExecute());
            this.NavigateToConditionDetailCommand = new Command(async (o) => await this.OnNavigateToConditionDetailCommandExecute(o));
        }

        public ICommand ViewRelatedPodsCommand
        {
            get;
        }

        public ICommand NavigateToConditionDetailCommand
        {
            get;
        }

        protected override Task<NodeDetail> GetObjectDetail(string name, string namespaceName) => this.KubernetesService.GetNodeDetail(name);

        private Task OnViewRelatedPodsCommandExecute() => this.NavigationService.NavigateToPodsPage($"spec.nodeName={this.Name}");

        private async Task OnNavigateToConditionDetailCommandExecute(object obj)
        {
            if (obj is Common.Services.Kubernetes.Model.Condition conditionDetail)
            {
                await this.NavigationService.NavigateToConditionDetailPage(conditionDetail);
            }
        }
    }
}
