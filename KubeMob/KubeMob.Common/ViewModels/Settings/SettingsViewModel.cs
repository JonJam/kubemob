using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using KubeMob.Common.Services.AccountManagement;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.Settings
{
    [Preserve(AllMembers = true)]
    public class SettingsViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private readonly IKubernetesService kubernetesService;
        private readonly IEnumerable<IAccountManager> accountManagers;

        public SettingsViewModel(
            INavigationService navigationService,
            IKubernetesService kubernetesService,
            IEnumerable<IAccountManager> accountManagers)
        {
            this.navigationService = navigationService;
            this.kubernetesService = kubernetesService;
            this.accountManagers = accountManagers;

            this.NavigateToObjectListingCommand =
                new Command(async () => await navigationService.NavigateToObjectListingPage());

            this.SwitchClusterCommand =
                new Command(async () => await this.OnSwitchClusterCommandExecute());
        }

        public ICommand NavigateToObjectListingCommand
        {
            get;
        }

        public ICommand SwitchClusterCommand
        {
            get;
        }

        private Task OnSwitchClusterCommandExecute()
        {
            this.accountManagers.First().RemoveSelectedCluster();

            this.kubernetesService.ResetClient();

            return this.navigationService.NavigateToClustersPage();
        }
    }
}