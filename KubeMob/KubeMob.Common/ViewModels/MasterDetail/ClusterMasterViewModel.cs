using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Navigation;
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
            INavigationService navigationService)
        {
            this.kubernetesService = kubernetesService;
            this.navigationService = navigationService;
        }
        
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
                if (this.SetProperty(ref this.selectedNamespace, value))
                {
                    this.kubernetesService.SetSelectedNamespace(this.selectedNamespace);
                }
            }
        }

        public override async Task Initialize(object navigationData)
        {
            this.toggleShowMasterCommand = (ICommand)navigationData;

            this.Namespaces = await this.kubernetesService.GetNamespaces();

            // Avoiding set this again when initializing.
            this.selectedNamespace = this.Namespaces.First(n => n.IsDefault);
            this.NotifyPropertyChanged(() => this.SelectedNamespace);
        }
    }
}
