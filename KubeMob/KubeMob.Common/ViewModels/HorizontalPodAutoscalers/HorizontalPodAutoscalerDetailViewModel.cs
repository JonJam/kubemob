using System;
using System.Threading.Tasks;
using System.Windows.Input;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.HorizontalPodAutoscalers
{
    [Preserve(AllMembers = true)]
    public class HorizontalPodAutoscalerDetailViewModel : ObjectDetailViewModelBase<HorizontalPodAutoscalerDetail>
    {
        public HorizontalPodAutoscalerDetailViewModel(
            IKubernetesService kubernetesService,
            IPopupService popupService,
            INavigationService navigationService)
            : base(kubernetesService, popupService, navigationService) => this.NavigateToTargetCommand = new Command(async () => await this.OnNavigateToTargetCommandExecute());

        public ICommand NavigateToTargetCommand
        {
            get;
        }

        protected override Task<HorizontalPodAutoscalerDetail> GetObjectDetail(string name, string namespaceName) => this.KubernetesService.GetHorizontalPodAutoscalerDetail(name, namespaceName);

        private Task OnNavigateToTargetCommandExecute()
        {
            switch (this.Detail.Target.Kind)
            {
                case "Deployment":
                    return this.NavigationService.NavigateToDeploymentDetailPage(
                        this.Detail.Target.Name,
                        this.NamespaceName);
                case "ReplicaSet":
                    return this.NavigationService.NavigateToReplicaSetDetailPage(
                        this.Detail.Target.Name,
                        this.NamespaceName);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
