using System;
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
        private readonly INavigationService navigationService;

        private bool viewAccounts;
        private ObservableCollection<ClusterSummaryGroup> clusterGroups;
        private bool hasClusterGroups;

        public ClustersViewModel(
            IEnumerable<IAccountManager> accountManagers,
            INavigationService navigationService)
        {
            this.accountManagers = accountManagers;
            this.navigationService = navigationService;

            this.ClusterGroups = new ObservableCollection<ClusterSummaryGroup>();
            this.clusterGroups.CollectionChanged += this.OnClusterGroupsCollectionChanged;

            this.OnAppearingCommand = new Command(async () => await this.Refresh());
            this.AddAccountCommand = new Command(async () => await navigationService.NavigateToAddAccountPage());
            this.ViewToggleCommand = new Command(() => this.ViewAccounts = !this.ViewAccounts);
            this.ClusterSelectedCommand = new Command(ClustersViewModel.OnClusterSelected);
            this.AccountSelectedCommand = new Command(async (o) => await this.OnAccountSelected(o));
            this.EditAccountCommand = new Command(async (o) => await this.OnAccountSelected(o));
            this.DeleteAccountCommand = new Command(this.OnDeleteAccount);
        }

        public ICommand OnAppearingCommand { get; }

        public ICommand AddAccountCommand { get; }

        public ICommand ViewToggleCommand { get; }

        public ICommand ClusterSelectedCommand { get; }

        public ICommand AccountSelectedCommand { get; }

        public ICommand EditAccountCommand { get; }

        public ICommand DeleteAccountCommand { get; }

        public bool ViewAccounts
        {
            get => this.viewAccounts;
            private set => this.SetProperty(ref this.viewAccounts, value);
        }

        public ObservableCollection<ClusterSummaryGroup> ClusterGroups
        {
            get => this.clusterGroups;
            private set => this.SetProperty(ref this.clusterGroups, value);
        }

        public bool HasClusterGroups
        {
            get => this.hasClusterGroups;
            set => this.SetProperty(ref this.hasClusterGroups, value);
        }

        private static void OnClusterSelected(object obj)
        {
            if (obj is ClusterSummary cluster)
            {
                // TODO Save selected cluster information.
                // TODO Navigate to Cluster overview page, with clean backstack.
            }
        }

        private async Task OnAccountSelected(object obj)
        {
            ClusterSummaryGroup clusterGroup = (ClusterSummaryGroup)obj;

            switch (clusterGroup.AccountType)
            {
                case AccountType.Azure:
                    await this.navigationService.NavigateToAddEditAzureAccountPage(clusterGroup.AccountId);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void OnDeleteAccount(object obj)
        {
            ClusterSummaryGroup clusterGroup = (ClusterSummaryGroup)obj;

            IAccountManager accountManager = this.accountManagers.First(am => am.HandlesAccountType(clusterGroup.AccountType));

            accountManager.RemoveAccount(clusterGroup.AccountId);

            this.ClusterGroups.Remove(clusterGroup);
        }

        private void OnClusterGroupsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => this.HasClusterGroups = this.ClusterGroups.Count > 0;

        private async Task Refresh()
        {
            this.IsBusy = true;

            // Resetting to false so displays individial clusters, after an add/edit to accounts.
            this.ViewAccounts = false;

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