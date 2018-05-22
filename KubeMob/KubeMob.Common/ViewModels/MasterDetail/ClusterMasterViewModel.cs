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
        private readonly NamespaceSelectorViewModel namespaceSelectorViewModel;

        public ClusterMasterViewModel(
            INavigationService navigationService,
            NamespaceSelectorViewModel namespaceSelectorViewModel)
        {
            this.namespaceSelectorViewModel = namespaceSelectorViewModel;

            this.MenuItemSelected = new Command(ClusterMasterViewModel.OnMenuItemSelected);

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
                        new Command(async () => await navigationService.NavigateToCronJobsPage())),

                    new ObjectTypeMenuItemViewModel(
                        AppResources.ClusterMasterViewModel_Workloads_DaemonSets,
                        new Command(async () => await navigationService.NavigateToDaemonSetsPage())),

                    new ObjectTypeMenuItemViewModel(
                        AppResources.ClusterMasterViewModel_Workloads_Deployments,
                        new Command(async () => await navigationService.NavigateToDeploymentsPage())),

                    new ObjectTypeMenuItemViewModel(
                        AppResources.ClusterMasterViewModel_Workloads_Jobs,
                        new Command(async () => await navigationService.NavigateToJobsPage())),

                    new ObjectTypeMenuItemViewModel(
                        AppResources.ClusterMasterViewModel_Workloads_Pods,
                        new Command(async () => await navigationService.NavigateToPodsPage())),

                    new ObjectTypeMenuItemViewModel(
                        AppResources.ClusterMasterViewModel_Workloads_ReplicaSets,
                        new Command(async () => await navigationService.NavigateToReplicaSetsPage())),

                    new ObjectTypeMenuItemViewModel(
                        AppResources.ClusterMasterViewModel_Workloads_ReplicationControllers,
                        new Command(async () => await navigationService.NavigateToReplicationControllersPage())),

                    new ObjectTypeMenuItemViewModel(
                        AppResources.ClusterMasterViewModel_Workloads_StatefulSets,
                        new Command(async () => await navigationService.NavigateToStatefulSetsPage()))
                },

                new MenuItemGroup(AppResources.ClusterMasterViewModel_DiscoveryAndLoadBalancing)
                {
                    new ObjectTypeMenuItemViewModel(
                        AppResources.ClusterMasterViewModel_DiscoveryAndLoadBalancing_Ingresses,
                        new Command(async () => await navigationService.NavigateToIngressesPage())),

                    new ObjectTypeMenuItemViewModel(
                        AppResources.ClusterMasterViewModel_DiscoveryAndLoadBalancing_Services,
                        new Command(async () => await navigationService.NavigateToServicesPage()))
                },

                new MenuItemGroup(AppResources.ClusterMasterViewModel_ConfigAndStorage)
                {
                    new ObjectTypeMenuItemViewModel(
                        AppResources.ClusterMasterViewModel_ConfigAndStorage_ConfigMaps,
                        new Command(async () => await navigationService.NavigateToConfigMapsPage())),

                    new ObjectTypeMenuItemViewModel(
                        AppResources.ClusterMasterViewModel_ConfigAndStorage_PersistentVolumeClaims,
                        new Command(async () => await navigationService.NavigateToPersistentVolumeClaimsPage())),

                    new ObjectTypeMenuItemViewModel(
                        AppResources.ClusterMasterViewModel_ConfigAndStorage_Secrets,
                        new Command(async () => await navigationService.NavigateToSecretsPage()))
                },

                new MenuItemGroup(string.Empty)
                {
                    new SettingsMenuItemViewModel(
                        AppResources.ClusterMasterViewModel_Settings,
                        new Command(async () => await navigationService.NavigateToSettingsPage()))
                }
            });
        }

        public ICommand MenuItemSelected
        {
            get;
        }

        public List<MenuItemGroup> MenuItems
        {
            get;
        }

        public override Task Initialize(object navigationData) => this.namespaceSelectorViewModel.Initialize(navigationData);

        private static void OnMenuItemSelected(object obj)
        {
            if (obj is ObjectTypeMenuItemViewModel menuItem)
            {
                menuItem.Command.Execute(null);
            }
        }
    }
}
