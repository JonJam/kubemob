using System.Threading.Tasks;

namespace KubeMob.Common.Services.Navigation
{
    /// <summary>
    /// Based off <see cref="https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/navigation#navigating-between-pages"/>
    /// </summary>
    public interface INavigationService
    {
        Task Initialize();

        Task NavigateToAddAccountPage();

        Task NavigateToAddEditAzureAccountPage(string id = null);

        Task NavigateToClusterPage();

        Task NavigateToPodsPage();

        Task NavigateToDeploymentsPage();

        Task NavigateToReplicaSetsPage();

        Task NavigateToServicesPage();

        Task NavigateToIngressesPage();

        Task NavigateToConfigMapsPage();

        Task NavigateToSecretsPage();

        Task NavigateToCronJobsPage();

        Task NavigateToDaemonSetsPage();

        Task NavigateToJobsPage();

        Task NavigateToReplicationControllersPage();

        Task RemoveLastFromBackStack();

        Task RemoveBackStack();

        Task GoBackToClustersPage();
    }
}
