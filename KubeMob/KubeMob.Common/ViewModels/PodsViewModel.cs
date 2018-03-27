using System.Windows.Input;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels
{
    [Preserve(AllMembers = true)]
    public class PodsViewModel : ViewModelBase
    {
        public PodsViewModel(
            INavigationService navigationService) => this.ViewPodDetailsCommand = new Command(async () => await navigationService.NavigateToPodDetailPage());

        public ICommand ViewPodDetailsCommand { get; }
    }
}
