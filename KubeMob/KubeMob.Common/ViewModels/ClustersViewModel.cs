using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using KubeMob.Common.Services.AccountManagement;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels
{
    [Preserve(AllMembers = true)]
    public class ClustersViewModel : ViewModelBase
    {
        private readonly IEnumerable<IAccountManager> accountManagers;

        private ObservableCollection<ClusterSummaryGroup> clusterGroups;

        private bool isInitialized;

        public ClustersViewModel(
            IEnumerable<IAccountManager> accountManagers,
            INavigationService navigationService)
        {
            this.accountManagers = accountManagers;

            this.ClusterGroups = new ObservableCollection<ClusterSummaryGroup>();

            this.OnAppearingCommand = new Command(async () => await this.OnAppearing());
            this.AddAccountCommand = new Command(async () => await navigationService.NavigateToAddAccountPage());
            this.ClusterSelectedCommand = new Command(ClustersViewModel.OnClusterSelected);
        }

        public ICommand OnAppearingCommand { get; }

        public ICommand AddAccountCommand { get; }

        public ICommand ClusterSelectedCommand { get; }

        public ObservableCollection<ClusterSummaryGroup> ClusterGroups
        {
            get => this.clusterGroups;
            private set => this.SetProperty(ref this.clusterGroups, value);
        }

        public override async Task Initialize(object navigationData)
        {
            await this.Refresh();

            this.isInitialized = true;
        }

        private static void OnClusterSelected(object obj)
        {
            if (obj is ClusterSummary cluster)
            {
                // TODO Save selected cluster information.
                // TODO Navigate to Cluster overview page, with clean backstack.
            }
        }

        private async Task Refresh()
        {
            this.IsBusy = true;

            this.ClusterGroups.Clear();

            // TODO Do on background thread?
            // TODO error handling.
            IEnumerable<Task> gettingClusters = this.accountManagers.Select(this.PopulateClusterGroups);

            await Task.WhenAny(gettingClusters);

            this.IsBusy = false;
        }

        private Task OnAppearing() => this.isInitialized ? this.Refresh() : Task.CompletedTask;

        private async Task PopulateClusterGroups(IAccountManager accountManager)
        {
            IEnumerable<ClusterSummaryGroup> clusterSummaryGroups = await accountManager.GetClusters();

            clusterSummaryGroups.ForEach(c => this.ClusterGroups.Add(c));
        }
    }
}
