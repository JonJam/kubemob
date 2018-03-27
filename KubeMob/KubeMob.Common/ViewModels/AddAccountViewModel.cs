using System.Windows.Input;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels
{
    [Preserve(AllMembers = true)]
    public class AddAccountViewModel : ViewModelBase
    {
        public AddAccountViewModel(
            INavigationService navigationService) => this.AddAzureAccountCommand = new Command(async () => await navigationService.NavigateToAddAzureAccountPage());

        public ICommand AddAzureAccountCommand { get; }
    }
}