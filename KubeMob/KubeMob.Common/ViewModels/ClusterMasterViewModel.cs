﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using KubeMob.Common.Resx;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels
{
    [Preserve(AllMembers = true)]
    public class ClusterMasterViewModel : ViewModelBase
    {
        public ClusterMasterViewModel(
            INavigationService navigationService)
        {
            this.MenuItemSelected = new Command(ClusterMasterViewModel.OnMenuItemSelected);

            this.MenuItems = new ObservableCollection<MenuItemViewModel>(new[]
            {
                new MenuItemViewModel(AppResources.ClusterMaster_Pods, new Command(async () => await navigationService.NavigateToPodsPage()))
            });
        }

        public ICommand MenuItemSelected { get; }

        public ObservableCollection<MenuItemViewModel> MenuItems { get; }

        private static void OnMenuItemSelected(object obj)
        {
            if (obj is MenuItemViewModel menuItem)
            {
                menuItem.Command.Execute(null);
            }
        }
    }
}
