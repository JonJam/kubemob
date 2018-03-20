using System.Collections.ObjectModel;
using System.Windows.Input;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels
{
    [Preserve(AllMembers = true)]
    public class ClusterMasterViewModel : ViewModelBase
    {
        private ObservableCollection<MenuItemViewModel> menuItems;

        public ClusterMasterViewModel(
            INavigationService navigationService)
        {
            this.MenuItemSelected = new Command(this.OnMenuItemSelected);

            this.MenuItems = new ObservableCollection<MenuItemViewModel>(new[]
            {
                //AppResources.ClusterMaster_Pods
                new MenuItemViewModel("Test", new Command(async () => await navigationService.NavigateToPodsPage()))
            });
        }

        public ICommand MenuItemSelected { get; }

        public ObservableCollection<MenuItemViewModel> MenuItems
        {
            get => this.menuItems;
            private set => this.SetProperty(ref this.menuItems, value);
        }

        private void OnMenuItemSelected(object obj)
        {
            if (obj is MenuItemViewModel menuItem)
            {
                menuItem.Command.Execute(null);
            }
        }
    }
}
