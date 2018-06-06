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

        Task NavigateToClusterOverviewPage();

        Task NavigateToPodsPage();

        Task NavigateToPodDetailPage(string name, string namespaceName);

        Task NavigateToDeploymentsPage();

        Task NavigateToDeploymentDetailPage(string name, string namespaceName);

        Task NavigateToReplicaSetsPage();

        Task NavigateToReplicaSetDetailPage(string name, string namespaceName);

        Task NavigateToServicesPage();

        Task NavigateToServiceDetailPage(string name, string namespaceName);

        Task NavigateToIngressesPage();

        Task NavigateToConfigMapsPage();

        Task NavigateToSecretsPage();

        Task NavigateToCronJobsPage();

        Task NavigateToCronJobDetailPage(string name, string namespaceName);

        Task NavigateToDaemonSetsPage();

        Task NavigateToDaemonSetDetailPage(string name, string namespaceName);

        Task NavigateToJobsPage();

        Task NavigateToReplicationControllersPage();

        Task NavigateToReplicationControllerDetailPage(string name, string namespaceName);

        Task NavigateToStatefulSetsPage();

        Task NavigateToPersistentVolumeClaimsPage();

        Task NavigateToSettingsPage();

        Task NavigateToResourceListingPage();

        Task RemoveLastFromBackStack();

        Task RemoveBackStack();

        Task GoBackToClustersPage();

        Task NavigateToClustersPage();
    }
}
