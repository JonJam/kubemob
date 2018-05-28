using System.Windows.Input;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.Settings
{
    [Preserve(AllMembers = true)]
    public class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel(INavigationService navigationService)
        {
            this.NavigateToResourceListingCommand =
                new Command(async () => await navigationService.NavigateToResourceListingPage());

            this.SwitchClusterCommand =
                new Command(() =>
                {
                });
        }

        public ICommand NavigateToResourceListingCommand
        {
            get;
        }

        public ICommand SwitchClusterCommand
        {
            get;
        }
    }
}