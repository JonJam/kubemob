using System.Threading.Tasks;
using System.Windows.Input;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.Pods
{
    [Preserve(AllMembers = true)]
    public class PodDetailViewModel : ObjectDetailViewModelBase<PodDetail>
    {
        public PodDetailViewModel(
            IKubernetesService kubernetesService,
            IPopupService popupService,
            INavigationService navigationService)
            : base(kubernetesService, popupService, navigationService) => this.NavigateToConditionDetailCommand = new Command(async (o) => await this.OnNavigateToConditionDetailCommandExecute(o));

        public ICommand NavigateToConditionDetailCommand
        {
            get;
        }

        protected override Task<PodDetail> GetObjectDetail(string name, string namespaceName) => this.KubernetesService.GetPodDetail(name, namespaceName);

        private async Task OnNavigateToConditionDetailCommandExecute(object obj)
        {
            if (obj is Common.Services.Kubernetes.Model.Condition conditionDetail)
            {
                await this.NavigationService.NavigateToConditionDetailPage(conditionDetail);
            }
        }
    }
}
