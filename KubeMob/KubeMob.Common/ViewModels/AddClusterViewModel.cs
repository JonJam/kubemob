using System.Windows.Input;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels
{
    [Preserve(AllMembers = true)]
    public class AddClusterViewModel : ViewModelBase
    {
        public AddClusterViewModel(
            INavigationService navigationService)
        {
            this.AddClusterCommand = new Command(() => navigationService.GoBack());
        }

        public ICommand AddClusterCommand { get; }
    }
}