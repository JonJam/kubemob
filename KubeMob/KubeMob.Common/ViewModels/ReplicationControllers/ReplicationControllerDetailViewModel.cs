using System.Threading.Tasks;
using System.Windows.Input;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
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
            this.ViewRelatedServicesCommand = new Command(async () => await this.OnViewRelatedServicesCommandExecute());
            this.ViewRelatedHorizontalPodAutoscalersCommand = new Command(async () => await this.OnViewHorizontalPodAutoscalersCommandExecute());
        }

        public ICommand ViewRelatedServicesCommand
        {
            get;
        }

        public ICommand ViewRelatedHorizontalPodAutoscalersCommand
        {
            get;
        }

        protected override Task<ReplicationControllerDetail> GetObjectDetail(string name, string namespaceName) => this.KubernetesService.GetReplicationControllerDetail(name, namespaceName);

        private Task OnViewRelatedServicesCommandExecute()
        {
            Filter filter = new Filter(
                this.NamespaceName,
                other: this.Detail.RelatedSelector);

            return this.NavigationService.NavigateToServicesPage(filter);
        }

        private Task OnViewHorizontalPodAutoscalersCommandExecute()
        {
            Filter filter = new Filter(other: this.Name);

            return this.NavigationService.NavigateToHorizontalPodAutoscalersPage(filter);
        }
    }
}
