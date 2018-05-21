using System.Threading.Tasks;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.MasterDetail
{
    [Preserve(AllMembers = true)]
    public class ClusterMasterDetailViewModel : ViewModelBase
    {
        public ClusterMasterDetailViewModel(
            ClusterMasterViewModel masterViewModel) => this.MasterViewModel = masterViewModel;

        public ClusterMasterViewModel MasterViewModel
        {
            get;
        }

        public override Task Initialize(object navigationData) => this.MasterViewModel.Initialize(navigationData);
    }
}
