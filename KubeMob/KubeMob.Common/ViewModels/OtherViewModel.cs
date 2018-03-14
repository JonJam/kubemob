using System.Windows.Input;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels
{
    [Preserve(AllMembers = true)]
    public class OtherViewModel : ViewModelBase
    {
        public OtherViewModel(
            INavigationService navigationService)
        {
            this.GoBackCommand = new Command(() => navigationService.GoBack());
        }

        public ICommand GoBackCommand { get; }
    }
}
