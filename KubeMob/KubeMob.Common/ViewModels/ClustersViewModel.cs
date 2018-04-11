using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using KubeMob.Common.Services.AccountManagement.Azure;
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
        // TODO refactor to use account manager interface and get list of them e.g. when if add GCE, AWS
        private readonly IAzureAccountManager accountManager;

        private IEnumerable<ClusterSummaryGroup> clusterGroups;

        public ClustersViewModel(
            IAzureAccountManager accountManager,
            INavigationService navigationService)
        {
            this.accountManager = accountManager;

            this.AddAccountCommand = new Command(async () => await navigationService.NavigateToAddAccountPage());
            this.ClusterSelectedCommand = new Command(ClustersViewModel.OnClusterSelected);
        }

        public ICommand AddAccountCommand { get; }

        public ICommand ClusterSelectedCommand { get; }

        public IEnumerable<ClusterSummaryGroup> ClusterGroups
        {
            get => this.clusterGroups;
            private set => this.SetProperty(ref this.clusterGroups, value);
        }

        // TODO Need to refresh this page on appearing, e.g. being navigated back from addazureaccount.
        public override async Task Initialize(object navigationData)
        {
            // TODO Do on background thread?
            this.ClusterGroups = await this.accountManager.GetClusters();
        }

        private static void OnClusterSelected(object obj)
        {
            if (obj is ClusterSummary cluster)
            {
                // TODO Save selected cluster information.
                // TODO Navigate to Cluster overview page, with clean backstack.
            }
        }
    }
}
