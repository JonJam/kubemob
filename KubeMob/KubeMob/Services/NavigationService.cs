using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KubeMob.Services
{
    public class NavigationService : INavigationService
    {
        public Task InitializeAsync()
        {
            throw new NotImplementedException();
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            throw new NotImplementedException();
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            throw new NotImplementedException();
        }

        public Task RemoveLastFromBackStackAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveBackStackAsync()
        {
            throw new NotImplementedException();
        }
    }
}
