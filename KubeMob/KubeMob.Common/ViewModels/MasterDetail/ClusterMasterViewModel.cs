using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Services.PubSub;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.MasterDetail
{
    [Preserve(AllMembers = true)]
    public class ClusterMasterViewModel : ViewModelBase
    {
        private readonly IKubernetesService kubernetesService;
        private readonly INavigationService navigationService;

        private ICommand toggleShowMasterCommand;
        private IList<Namespace> namespaces;
        private Namespace selectedNamespace;

        public ClusterMasterViewModel(
            IKubernetesService kubernetesService,
            INavigationService navigationService,
            IPubSubService pubSubService)
        {
            this.kubernetesService = kubernetesService;
            this.navigationService = navigationService;

            pubSubService.SubscribeToResourceListingSettingChanged<IKubernetesService>(
                this,
                this.HandleResourceListingSettingChanged);
        }

        public ICommand NavigateToNamespacesCommand => new Command(async () =>
        {
            this.toggleShowMasterCommand.Execute(null);
            await this.navigationService.NavigateToNamespacesPage();
        });

        public ICommand NavigateToNodesCommand => new Command(async () =>
        {
            this.toggleShowMasterCommand.Execute(null);
            await this.navigationService.NavigateToNodesPage();
        });

        public ICommand NavigateToPersistentVolumesCommand => new Command(async () =>
        {
            this.toggleShowMasterCommand.Execute(null);
            await this.navigationService.NavigateToPersistentVolumesPage();
        });

        public ICommand NavigateToStorageClassesCommand => new Command(async () =>
        {
            this.toggleShowMasterCommand.Execute(null);
            await this.navigationService.NavigateToStorageClassesPage();
        });

        public ICommand NavigateToCronJobsCommand => new Command(async () =>
            {
                this.toggleShowMasterCommand.Execute(null);
                await this.navigationService.NavigateToCronJobsPage();
            });

        public ICommand NavigateToDaemonSetsCommand =>
            new Command(async () =>
            {
                this.toggleShowMasterCommand.Execute(null);
                await this.navigationService.NavigateToDaemonSetsPage();
            });

        public ICommand NavigateToDeploymentsCommand =>
            new Command(async () =>
            {
                this.toggleShowMasterCommand.Execute(null);
                await this.navigationService.NavigateToDeploymentsPage();
            });

        public ICommand NavigateToJobsCommand =>
            new Command(async () =>
            {
                this.toggleShowMasterCommand.Execute(null);
                await this.navigationService.NavigateToJobsPage();
            });

        public ICommand NavigateToPodsCommand =>
            new Command(async () =>
            {
                this.toggleShowMasterCommand.Execute(null);
                await this.navigationService.NavigateToPodsPage();
            });

        public ICommand NavigateToReplicaSetsCommand =>
            new Command(async () =>
            {
                this.toggleShowMasterCommand.Execute(null);
                await this.navigationService.NavigateToReplicaSetsPage();
            });

        public ICommand NavigateToReplicationControllersCommand =>
            new Command(async () =>
            {
                this.toggleShowMasterCommand.Execute(null);
                await this.navigationService.NavigateToReplicationControllersPage();
            });

        public ICommand NavigateToStatefulSetsCommand =>
            new Command(async () =>
            {
                this.toggleShowMasterCommand.Execute(null);
                await this.navigationService.NavigateToStatefulSetsPage();
            });

        public ICommand NavigateToIngressesCommand =>
            new Command(async () =>
            {
                this.toggleShowMasterCommand.Execute(null);
                await this.navigationService.NavigateToIngressesPage();
            });

        public ICommand NavigateToServicesCommand =>
            new Command(async () =>
            {
                this.toggleShowMasterCommand.Execute(null);
                await this.navigationService.NavigateToServicesPage();
            });

        public ICommand NavigateToConfigMapsCommand =>
            new Command(async () =>
            {
                this.toggleShowMasterCommand.Execute(null);
                await this.navigationService.NavigateToConfigMapsPage();
            });

        public ICommand NavigateToPersistentVolumeClaimsCommand =>
            new Command(async () =>
            {
                this.toggleShowMasterCommand.Execute(null);
                await this.navigationService.NavigateToPersistentVolumeClaimsPage();
            });

        public ICommand NavigateToSecretsCommand =>
            new Command(async () =>
            {
                this.toggleShowMasterCommand.Execute(null);
                await this.navigationService.NavigateToSecretsPage();
            });

        public ICommand NavigateToSettingsCommand =>
            new Command(async () =>
            {
                this.toggleShowMasterCommand.Execute(null);
                await this.navigationService.NavigateToSettingsPage();
            });

        public IList<Namespace> Namespaces
        {
            get => this.namespaces;
            private set => this.SetProperty(ref this.namespaces, value);
        }

        public Namespace SelectedNamespace
        {
            get => this.selectedNamespace;
            set
            {
                // value is null when navigating away from page e.g. to Settings, so
                // ignoring this so not to remove user set value.
                if (this.SetProperty(ref this.selectedNamespace, value) &&
                    value != null)
                {
                    this.kubernetesService.SetSelectedNamespace(this.selectedNamespace);
                }
            }
        }

        public bool ShowCluster => this.ShowNamespaces ||
                                     this.ShowNodes ||
                                     this.ShowPersistentVolumes ||
                                     this.ShowStorageClasses;

        public bool ShowNamespaces => this.kubernetesService.ShowNamespaces;

        public bool ShowNodes => this.kubernetesService.ShowNodes;

        public bool ShowPersistentVolumes => this.kubernetesService.ShowPersistentVolumes;

        public bool ShowStorageClasses => this.kubernetesService.ShowStorageClasses;

        public bool ShowWorkloads => this.ShowCronJobs ||
                                     this.ShowDaemonSets ||
                                     this.ShowDeployments ||
                                     this.ShowJobs ||
                                     this.ShowReplicaSets ||
                                     this.ShowReplicationControllers ||
                                     this.ShowStatefulSets;

        public bool ShowCronJobs => this.kubernetesService.ShowCronJobs;

        public bool ShowDaemonSets => this.kubernetesService.ShowDaemonSets;

        public bool ShowDeployments => this.kubernetesService.ShowDeployments;

        public bool ShowJobs => this.kubernetesService.ShowJobs;

        public bool ShowPods => this.kubernetesService.ShowPods;

        public bool ShowReplicaSets => this.kubernetesService.ShowReplicaSets;

        public bool ShowReplicationControllers => this.kubernetesService.ShowReplicationControllers;

        public bool ShowStatefulSets => this.kubernetesService.ShowStatefulSets;

        public bool ShowDiscoveryAndLoadBalancing => this.ShowIngresses ||
                                     this.ShowServices;

        public bool ShowIngresses => this.kubernetesService.ShowIngresses;

        public bool ShowServices => this.kubernetesService.ShowServices;

        public bool ShowConfigAndStorage => this.ShowConfigMaps ||
                                            this.ShowPersistentVolumeClaims ||
                                            this.ShowSecrets;

        public bool ShowConfigMaps => this.kubernetesService.ShowConfigMaps;

        public bool ShowPersistentVolumeClaims => this.kubernetesService.ShowPersistentVolumeClaims;

        public bool ShowSecrets => this.kubernetesService.ShowSecrets;

        public override async Task Initialize(object navigationData)
        {
            this.toggleShowMasterCommand = (ICommand)navigationData;

            this.Namespaces = await this.kubernetesService.GetNamespaces();

            // Avoiding set this again when initializing.
            this.selectedNamespace = this.Namespaces.First(n => n.IsDefault);
            this.NotifyPropertyChanged(() => this.SelectedNamespace);
        }

        private void HandleResourceListingSettingChanged(
            IKubernetesService sender,
            string settingName)
        {
            this.OnPropertyChanged(settingName);

            switch (settingName)
            {
                case nameof(this.ShowIngresses):
                case nameof(this.ShowServices):
                    this.NotifyPropertyChanged(() => this.ShowDiscoveryAndLoadBalancing);
                    break;
                case nameof(this.ShowConfigMaps):
                case nameof(this.ShowPersistentVolumeClaims):
                case nameof(this.ShowSecrets):
                    this.NotifyPropertyChanged(() => this.ShowConfigAndStorage);
                    break;
                case nameof(this.ShowNamespaces):
                case nameof(this.ShowNodes):
                case nameof(this.ShowPersistentVolumes):
                case nameof(this.ShowStorageClasses):
                    this.NotifyPropertyChanged(() => this.ShowCluster);
                    break;
                default:
                    this.NotifyPropertyChanged(() => this.ShowWorkloads);
                    break;
            }
        }
    }
}
