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
            ClusterMasterViewModel masterViewModel) => this.MasterViewModel = masterViewModel;

        public bool ShowMaster
        {
            get => this.showMaster;
            set => this.SetProperty(ref this.showMaster, value);
        }

        public ClusterMasterViewModel MasterViewModel
        {
            get;
        }

        private ICommand ToggleShowMasterCommand => new Command(() => this.ShowMaster = !this.showMaster);

        public override Task Initialize(object navigationData) => this.MasterViewModel.Initialize(this.ToggleShowMasterCommand);
    }
}
