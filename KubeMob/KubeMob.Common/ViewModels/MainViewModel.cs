using System.Windows.Input;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels
{
    [Preserve(AllMembers = true)]
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(
            INavigationService navigationService)
        {
            this.NavigateToOtherCommand = new Command(() => navigationService.NavigateToOtherPage());
        }

        public ICommand NavigateToOtherCommand { get; }
    }
}
