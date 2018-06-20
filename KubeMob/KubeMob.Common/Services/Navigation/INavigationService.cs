using System.Threading.Tasks;
using KubeMob.Common.Services.Kubernetes.Model;

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

        Task NavigateToNamespacesPage();

        Task NavigateToNamespaceDetailPage(string name);

        Task NavigateToPersistentVolumesPage();

        Task NavigateToPersistentVolumeDetailPage(string name);

        Task NavigateToNodesPage();

        Task NavigateToNodeDetailPage(string name);

        Task NavigateToStorageClassesPage();

        Task NavigateToStorageClassDetailPage(string name);

        Task NavigateToPodsPage();

        Task NavigateToPodDetailPage(string name, string namespaceName);

        Task NavigateToDeploymentsPage();

        Task NavigateToDeploymentDetailPage(string name, string namespaceName);

        Task NavigateToReplicaSetsPage();

        Task NavigateToReplicaSetDetailPage(string name, string namespaceName);

        Task NavigateToServicesPage();

        Task NavigateToServiceDetailPage(string name, string namespaceName);

        Task NavigateToIngressesPage();

        Task NavigateToIngressDetailPage(string name, string namespaceName);

        Task NavigateToConfigMapsPage();

        Task NavigateToConfigMapDetailPage(string name, string namespaceName);

        Task NavigateToSecretsPage();

        Task NavigateToSecretDetailPage(string name, string namespaceName);

        Task NavigateToCronJobsPage();

        Task NavigateToCronJobDetailPage(string name, string namespaceName);

        Task NavigateToDaemonSetsPage();

        Task NavigateToDaemonSetDetailPage(string name, string namespaceName);

        Task NavigateToJobsPage();

        Task NavigateToJobDetailPage(string name, string namespaceName);

        Task NavigateToReplicationControllersPage();

        Task NavigateToReplicationControllerDetailPage(string name, string namespaceName);

        Task NavigateToStatefulSetsPage();

        Task NavigateToStatefulSetDetailPage(string name, string namespaceName);

        Task NavigateToPersistentVolumeClaimsPage();

        Task NavigateToPersistentVolumeClaimDetailPage(string name, string namespaceName);

        Task NavigateToEventDetailPage(Event eventDetail);

        Task NavigateToSettingsPage();

        Task NavigateToResourceListingPage();

        Task RemoveLastFromBackStack();

        Task RemoveBackStack();

        Task GoBackToClustersPage();

        Task NavigateToClustersPage();
    }
}
