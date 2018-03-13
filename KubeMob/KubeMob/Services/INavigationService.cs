using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KubeMob.Services
{
    // TODO see Speedwell navigation service
    // TODO Change to reference view models

    public interface INavigationService
    {
        //ViewModelBase PreviousPageViewModel { get; }

        Task InitializeAsync();
        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;
        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;
        Task RemoveLastFromBackStackAsync();
        Task RemoveBackStackAsync();
    }
}
