using System.Collections.ObjectModel;
using System.Windows.Input;
using KubeMob.Common.Resx;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels
{
    [Preserve(AllMembers = true)]
    public class ClusterMasterViewModel : ViewModelBase
    {
        public ClusterMasterViewModel(
            INavigationService navigationService)
        {
            this.MenuItemSelected = new Command(ClusterMasterViewModel.OnMenuItemSelected);

            this.MenuItems = new ObservableCollection<MenuItemGroup>(new[]
            {
                // TODO Add Overview link
                new MenuItemGroup(AppResources.ClusterMasterViewModel_Workloads)
                {
                    new MenuItemViewModel(
                        AppResources.ClusterMasterViewModel_Workloads_CronJobs,
                        new Command(async () => await navigationService.NavigateToCronJobsPage())),

                    new MenuItemViewModel(
                        AppResources.ClusterMasterViewModel_Workloads_DaemonSets,
                        new Command(async () => await navigationService.NavigateToDaemonSetsPage())),

                    new MenuItemViewModel(
                        AppResources.ClusterMasterViewModel_Workloads_Deployments,
                        new Command(async () => await navigationService.NavigateToDeploymentsPage())),

                    new MenuItemViewModel(
                        AppResources.ClusterMasterViewModel_Workloads_Jobs,
                        new Command(async () => await navigationService.NavigateToJobsPage())),

                    new MenuItemViewModel(
                        AppResources.ClusterMasterViewModel_Workloads_Pods,
                        new Command(async () => await navigationService.NavigateToPodsPage())),

                    new MenuItemViewModel(
                        AppResources.ClusterMasterViewModel_Workloads_ReplicaSets,
                        new Command(async () => await navigationService.NavigateToReplicaSetsPage())),

                    new MenuItemViewModel(
                        AppResources.ClusterMasterViewModel_Workloads_ReplicationControllers,
                        new Command(async () => await navigationService.NavigateToReplicationControllersPage()))
                },

                new MenuItemGroup(AppResources.ClusterMasterViewModel_DiscoveryAndLoadBalancing)
                {
                    new MenuItemViewModel(
                        AppResources.ClusterMasterViewModel_DiscoveryAndLoadBalancing_Ingresses,
                        new Command(async () => await navigationService.NavigateToIngressesPage())),

                    new MenuItemViewModel(
                        AppResources.ClusterMasterViewModel_DiscoveryAndLoadBalancing_Services,
                        new Command(async () => await navigationService.NavigateToServicesPage()))
                },

                new MenuItemGroup(AppResources.ClusterMasterViewModel_ConfigAndStorage)
                {
                    new MenuItemViewModel(
                        AppResources.ClusterMasterViewModel_ConfigAndStorage_ConfigMaps,
                        new Command(async () => await navigationService.NavigateToConfigMapsPage())),

                    new MenuItemViewModel(
                        AppResources.ClusterMasterViewModel_ConfigAndStorage_PersistentVolumeClaims,
                        new Command(async () => await navigationService.NavigateToPersistentVolumeClaimsPage())),

                    new MenuItemViewModel(
                        AppResources.ClusterMasterViewModel_ConfigAndStorage_Secrets,
                        new Command(async () => await navigationService.NavigateToSecretsPage()))
                }
            });
        }

        public ICommand MenuItemSelected { get; }

        public ObservableCollection<MenuItemGroup> MenuItems { get; }

        private static void OnMenuItemSelected(object obj)
        {
            if (obj is MenuItemViewModel menuItem)
            {
                menuItem.Command.Execute(null);
            }
        }
    }
}
