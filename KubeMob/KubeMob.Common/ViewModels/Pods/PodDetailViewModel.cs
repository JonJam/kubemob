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
using Condition = KubeMob.Common.Services.Kubernetes.Model.Condition;

namespace KubeMob.Common.ViewModels.Pods
{
    [Preserve(AllMembers = true)]
    public class PodDetailViewModel : ObjectDetailViewModelBase<PodDetail>
    {
        // TODO Remove commands.
        public PodDetailViewModel(
            IKubernetesService kubernetesService,
            IPopupService popupService,
            INavigationService navigationService)
            : base(kubernetesService, popupService, navigationService)
        {
            this.NavigateToNodeDetailCommand =
                new Command(async (o) => await this.OnNavigateToNodeDetailCommandExecute(o));

            this.NavigateToConditionDetailCommand =
                new Command(async (o) => await this.OnNavigateToConditionDetailCommandExecute(o));

            this.NavigateToOwnerCommand = new Command(async (o) => await this.OnNavigateToOwnerCommandExecute(o));

            this.ViewRelatedPersistentVolumeClaimsCommand = new Command(async () => await this.OnViewRelatedPersistentVolumeClaimsCommandExecute());
        }

        public ICommand NavigateToNodeDetailCommand
        {
            get;
        }

        public ICommand NavigateToConditionDetailCommand
        {
            get;
        }

        public ICommand NavigateToOwnerCommand
        {
            get;
        }

        public ICommand ViewRelatedPersistentVolumeClaimsCommand
        {
            get;
        }

        protected override Task<PodDetail> GetObjectDetail(string name, string namespaceName) => this.KubernetesService.GetPodDetail(name, namespaceName);

        private Task OnNavigateToNodeDetailCommandExecute(object obj)
            => this.NavigationService.NavigateToNodeDetailPage(this.Detail.NodeName);

        private async Task OnNavigateToConditionDetailCommandExecute(object obj)
        {
            Condition conditionDetail = (Condition)obj;

            await this.NavigationService.NavigateToConditionDetailPage(conditionDetail);
        }

        private Task OnNavigateToOwnerCommandExecute(object obj)
        {
            ObjectReference owner = (ObjectReference)obj;

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

        private Task OnViewRelatedPersistentVolumeClaimsCommandExecute()
        {
            Filter filter = new Filter(
                this.Detail.NamespaceName,
                other: string.Join(",", this.Detail.PersistentVolumeClaims));

            return this.NavigationService.NavigateToPersistentVolumeClaimsPage(filter);
        }
    }
}
