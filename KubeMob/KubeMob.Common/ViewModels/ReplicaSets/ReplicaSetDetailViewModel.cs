using System.Threading.Tasks;
using System.Windows.Input;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.ReplicaSets
{
    [Preserve(AllMembers = true)]
    public class ReplicaSetDetailViewModel : ObjectDetailViewModelBase<ReplicaSetDetail>
    {
        public ReplicaSetDetailViewModel(
            IKubernetesService kubernetesService,
            IPopupService popupService,
            INavigationService navigationService)
            : base(kubernetesService, popupService, navigationService)
        {
            this.ViewRelatedPodsCommand = new Command(async () => await this.OnViewRelatedPodsCommandExecute());
            this.ViewRelatedServicesCommand = new Command(async () => await this.OnViewRelatedServicesCommandExecute());
            this.ViewRelatedHorizontalPodAutoscalersCommand = new Command(async () => await this.OnViewHorizontalPodAutoscalersCommandExecute());
        }

        public ICommand ViewRelatedPodsCommand
        {
            get;
        }

        public ICommand ViewRelatedServicesCommand
        {
            get;
        }

        public ICommand ViewRelatedHorizontalPodAutoscalersCommand
        {
            get;
        }

        protected override Task<ReplicaSetDetail> GetObjectDetail(string name, string namespaceName) => this.KubernetesService.GetReplicaSetDetail(name, namespaceName);

        private Task OnViewRelatedPodsCommandExecute()
        {
            Filter filter = new Filter(
                this.NamespaceName,
                other: this.Name);

            return this.NavigationService.NavigateToPodsPage(filter);
        }

        private Task OnViewRelatedServicesCommandExecute()
        {
            Filter filter = new Filter(
                this.NamespaceName,
                other: this.Detail.RelatedSelector);

            return this.NavigationService.NavigateToServicesPage(filter);
        }

        private Task OnViewHorizontalPodAutoscalersCommandExecute()
        {
            Filter filter = new Filter(
                this.NamespaceName,
                other: this.Name);

            return this.NavigationService.NavigateToHorizontalPodAutoscalersPage(filter);
        }
    }
}
