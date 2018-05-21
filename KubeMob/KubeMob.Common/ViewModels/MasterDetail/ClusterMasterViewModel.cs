using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using KubeMob.Common.Resx;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.MasterDetail
{
    [Preserve(AllMembers = true)]
    public class ClusterMasterViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private readonly NamespaceSelectorViewModel namespaceSelectorViewModel;

        private List<MenuItemGroup> menuItems;

        public ClusterMasterViewModel(
            INavigationService navigationService,
            NamespaceSelectorViewModel namespaceSelectorViewModel)
        {
            this.navigationService = navigationService;
            this.namespaceSelectorViewModel = namespaceSelectorViewModel;

            this.MenuItemSelected = new Command(ClusterMasterViewModel.OnMenuItemSelected);
        }

        public ICommand MenuItemSelected
        {
            get;
        }

        public List<MenuItemGroup> MenuItems
        {
            get => this.menuItems;
            private set => this.SetProperty(ref this.menuItems, value);
        }

        public override async Task Initialize(object navigationData)
        {
            await this.namespaceSelectorViewModel.Initialize(navigationData);

            this.MenuItems = new List<MenuItemGroup>(new[]
            {
                new MenuItemGroup(string.Empty)
                {
                    this.namespaceSelectorViewModel
                },

                new MenuItemGroup(AppResources.ClusterMasterViewModel_Workloads)
                {
                    new ObjectTypeMenuItemViewModel(
                        AppResources.ClusterMasterViewModel_Workloads_CronJobs,
                        new Command(async () => await this.navigationService.NavigateToCronJobsPage())),

                    new ObjectTypeMenuItemViewModel(
                        AppResources.ClusterMasterViewModel_Workloads_DaemonSets,
                        new Command(async () => await this.navigationService.NavigateToDaemonSetsPage())),

                    new ObjectTypeMenuItemViewModel(
                        AppResources.ClusterMasterViewModel_Workloads_Deployments,
                        new Command(async () => await this.navigationService.NavigateToDeploymentsPage())),

                    new ObjectTypeMenuItemViewModel(
                        AppResources.ClusterMasterViewModel_Workloads_Jobs,
                        new Command(async () => await this.navigationService.NavigateToJobsPage())),

                    new ObjectTypeMenuItemViewModel(
                        AppResources.ClusterMasterViewModel_Workloads_Pods,
                        new Command(async () => await this.navigationService.NavigateToPodsPage())),

                    new ObjectTypeMenuItemViewModel(
                        AppResources.ClusterMasterViewModel_Workloads_ReplicaSets,
                        new Command(async () => await this.navigationService.NavigateToReplicaSetsPage())),

                    new ObjectTypeMenuItemViewModel(
                        AppResources.ClusterMasterViewModel_Workloads_ReplicationControllers,
                        new Command(async () => await this.navigationService.NavigateToReplicationControllersPage())),

                    new ObjectTypeMenuItemViewModel(
                        AppResources.ClusterMasterViewModel_Workloads_StatefulSets,
                        new Command(async () => await this.navigationService.NavigateToStatefulSetsPage()))
                },

                new MenuItemGroup(AppResources.ClusterMasterViewModel_DiscoveryAndLoadBalancing)
                {
                    new ObjectTypeMenuItemViewModel(
                        AppResources.ClusterMasterViewModel_DiscoveryAndLoadBalancing_Ingresses,
                        new Command(async () => await this.navigationService.NavigateToIngressesPage())),

                    new ObjectTypeMenuItemViewModel(
                        AppResources.ClusterMasterViewModel_DiscoveryAndLoadBalancing_Services,
                        new Command(async () => await this.navigationService.NavigateToServicesPage()))
                },

                new MenuItemGroup(AppResources.ClusterMasterViewModel_ConfigAndStorage)
                {
                    new ObjectTypeMenuItemViewModel(
                        AppResources.ClusterMasterViewModel_ConfigAndStorage_ConfigMaps,
                        new Command(async () => await this.navigationService.NavigateToConfigMapsPage())),

                    new ObjectTypeMenuItemViewModel(
                        AppResources.ClusterMasterViewModel_ConfigAndStorage_PersistentVolumeClaims,
                        new Command(async () => await this.navigationService.NavigateToPersistentVolumeClaimsPage())),

                    new ObjectTypeMenuItemViewModel(
                        AppResources.ClusterMasterViewModel_ConfigAndStorage_Secrets,
                        new Command(async () => await this.navigationService.NavigateToSecretsPage()))
                }
            });
        }

        private static void OnMenuItemSelected(object obj)
        {
            if (obj is ObjectTypeMenuItemViewModel menuItem)
            {
                menuItem.Command.Execute(null);
            }
        }
    }
}
