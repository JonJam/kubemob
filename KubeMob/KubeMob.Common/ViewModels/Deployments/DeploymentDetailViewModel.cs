using System.Threading.Tasks;
using System.Windows.Input;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.Deployments
{
    [Preserve(AllMembers = true)]
    public class DeploymentDetailViewModel : ObjectDetailViewModelBase<DeploymentDetail>
    {
        // TODO Commands
        public DeploymentDetailViewModel(
            IKubernetesService kubernetesService,
            IPopupService popupService,
            INavigationService navigationService)
            : base(kubernetesService, popupService, navigationService)
        {
            this.ViewRelatedReplicaSetsCommand = new Command(async () => await this.OnViewRelatedReplicaSetsCommandExecute());
            this.ViewRelatedHorizontalPodAutoscalersCommand = new Command(async () => await this.OnViewHorizontalPodAutoscalersCommandExecute());
        }

        public ICommand ViewRelatedReplicaSetsCommand
        {
            get;
        }

        public ICommand ViewRelatedHorizontalPodAutoscalersCommand
        {
            get;
        }

        protected override Task<DeploymentDetail> GetObjectDetail(string name, string namespaceName) => this.KubernetesService.GetDeploymentDetail(name, namespaceName);

        private Task OnViewRelatedReplicaSetsCommandExecute()
        {
            Filter filter = new Filter(
                this.Detail.NamespaceName,
                other: this.Detail.Uid);

            return this.NavigationService.NavigateToReplicaSetsPage(filter);
        }

        private Task OnViewHorizontalPodAutoscalersCommandExecute()
        {
            Filter filter = new Filter(
                this.Detail.NamespaceName,
                other: this.Detail.Name);

            return this.NavigationService.NavigateToHorizontalPodAutoscalersPage(filter);
        }
    }
}
