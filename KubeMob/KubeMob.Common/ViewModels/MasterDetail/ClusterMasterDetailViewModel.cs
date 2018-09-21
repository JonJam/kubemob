using System.Threading.Tasks;
using System.Windows.Input;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.MasterDetail
{
    [Preserve(AllMembers = true)]
    public class ClusterMasterDetailViewModel : ViewModelBase
    {
        private bool showMaster;

        public ClusterMasterDetailViewModel(
            ClusterMasterViewModel masterViewModel,
            ClusterOverviewViewModel clusterOverviewViewModel)
        {
            this.MasterViewModel = masterViewModel;
            this.ClusterOverviewViewModel = clusterOverviewViewModel;
        }

        public bool ShowMaster
        {
            get => this.showMaster;
            set => this.SetProperty(ref this.showMaster, value);
        }

        public ClusterMasterViewModel MasterViewModel
        {
            get;
        }

        public ClusterOverviewViewModel ClusterOverviewViewModel
        {
            get;
        }

        private ICommand ToggleShowMasterCommand => new Command(() => this.ShowMaster = !this.showMaster);

        public override Task Initialize(object navigationData)
        {
            Task masterViewModelTask = this.MasterViewModel.Initialize(this.ToggleShowMasterCommand);
            Task clusterOverviewViewModelTask = this.ClusterOverviewViewModel.Initialize(null);

            return Task.WhenAll(masterViewModelTask, clusterOverviewViewModelTask);
        }
    }
}