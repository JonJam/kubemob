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
            INavigationService navigationService) => this.AddAccountCommand = new Command(async () => await navigationService.NavigateToAddAccountPage());

        public ICommand AddAccountCommand { get; }
    }
}
