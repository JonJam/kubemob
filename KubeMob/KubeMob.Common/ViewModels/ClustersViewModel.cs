using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
        private bool hasClusterGroups;

        public ClustersViewModel(
            IEnumerable<IAccountManager> accountManagers,
            INavigationService navigationService)
        {
            this.accountManagers = accountManagers;

            this.ClusterGroups = new ObservableCollection<ClusterSummaryGroup>();
            this.clusterGroups.CollectionChanged += this.OnClusterGroupsCollectionChanged;

            this.OnAppearingCommand = new Command(async () => await this.Refresh());
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

        public bool HasClusterGroups
        {
            get => this.hasClusterGroups;
            set
            {
                if (this.SetProperty(ref this.hasClusterGroups, value))
                {
                    this.NotifyPropertyChanged(() => this.IsClusterGroupsEmpty);
                }
            }
        }

        public bool IsClusterGroupsEmpty => !this.hasClusterGroups;

        private static void OnClusterSelected(object obj)
        {
            if (obj is ClusterSummary cluster)
            {
                // TODO Save selected cluster information.
                // TODO Navigate to Cluster overview page, with clean backstack.
            }
        }

        private void OnClusterGroupsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => this.HasClusterGroups = this.ClusterGroups.Count > 0;

        private async Task Refresh()
        {
            this.IsBusy = true;

            this.ClusterGroups.Clear();

            IEnumerable<Task> gettingClusters = this.accountManagers.Select(this.PopulateClusterGroups);

            await Task.WhenAny(gettingClusters);

            this.IsBusy = false;
        }

        private async Task PopulateClusterGroups(IAccountManager accountManager)
        {
            IEnumerable<ClusterSummaryGroup> clusterSummaryGroups = await accountManager.GetClusters();

            clusterSummaryGroups.ForEach(c => this.ClusterGroups.Add(c));
        }
    }
}