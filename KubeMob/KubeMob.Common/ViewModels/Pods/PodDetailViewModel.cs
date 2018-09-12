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

namespace KubeMob.Common.ViewModels.Pods
{
    [Preserve(AllMembers = true)]
    public class PodDetailViewModel : ObjectDetailViewModelBase<PodDetail>
    {
        public PodDetailViewModel(
            IKubernetesService kubernetesService,
            IPopupService popupService,
            INavigationService navigationService)
            : base(kubernetesService, popupService, navigationService)
        {
            this.NavigateToNodeDetailCommand =
                new Command(async (o) => await this.OnNavigateToNodeDetailCommandExecute(o));

            this.NavigateToOwnerCommand = new Command(async (o) => await this.OnNavigateToOwnerCommandExecute(o));
        }

        public ICommand NavigateToNodeDetailCommand
        {
            get;
        }

        public ICommand NavigateToOwnerCommand
        {
            get;
        }

        protected override Task<PodDetail> GetObjectDetail(string name, string namespaceName) => this.KubernetesService.GetPodDetail(name, namespaceName);

        private Task OnNavigateToNodeDetailCommandExecute(object obj)
            => this.NavigationService.NavigateToNodeDetailPage(this.Detail.NodeName);

        private Task OnNavigateToOwnerCommandExecute(object obj)
        {
            ObjectReference owner = this.Detail.Owner;

            switch (owner.Kind)
            {
                case "DaemonSet":
                    return this.NavigationService.NavigateToDaemonSetDetailPage(owner.Name, this.Detail.NamespaceName);
                case "StatefulSet":
                    return this.NavigationService.NavigateToStatefulSetDetailPage(owner.Name, this.Detail.NamespaceName);
                case "ReplicaSet":
                    return this.NavigationService.NavigateToReplicaSetDetailPage(owner.Name, this.Detail.NamespaceName);
                case "ReplicationController":
                    return this.NavigationService.NavigateToReplicationControllerDetailPage(owner.Name, this.Detail.NamespaceName);
                case "Job":
                    return this.NavigationService.NavigateToJobDetailPage(owner.Name, this.Detail.NamespaceName);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
