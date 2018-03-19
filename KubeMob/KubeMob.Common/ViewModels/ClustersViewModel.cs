using System.Windows.Input;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels
{
    [Preserve(AllMembers = true)]
    public class ClustersViewModel : ViewModelBase
    {
        public ClustersViewModel(
            INavigationService navigationService)
        {
            this.AddClusterCommand = new Command(async () => await navigationService.NavigateToAddClusterPage());
            this.SelectClusterCommand = new Command(async () => await navigationService.NavigateToClusterPage());
        }

        public ICommand AddClusterCommand { get; }

        public ICommand SelectClusterCommand { get; }
    }
}
