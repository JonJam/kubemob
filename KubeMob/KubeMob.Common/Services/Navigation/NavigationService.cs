using System;
using System.Threading.Tasks;
using KubeMob.Common.Pages;
using KubeMob.Common.Pages.Base;
using KubeMob.Common.Pages.CronJobs;
using KubeMob.Common.Pages.DaemonSets;
using KubeMob.Common.Pages.Deployments;
using KubeMob.Common.Pages.MasterDetail;
using KubeMob.Common.Pages.Pods;
using KubeMob.Common.Pages.ReplicaSets;
using KubeMob.Common.Pages.ReplicationControllers;
using KubeMob.Common.Pages.Services;
using KubeMob.Common.Pages.Settings;
using KubeMob.Common.Services.Settings;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Navigation
{
    /// <summary>
    /// Based off <see cref="https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/navigation#creating-the-navigationservice-instance"/>
    /// </summary>
    public class NavigationService : INavigationService
    {
        private readonly IAppSettings appSettings;

        [Preserve]
        public NavigationService(
            IAppSettings appSettings) => this.appSettings = appSettings;

        public Task Initialize()
        {
            Type pageType = typeof(ClustersPage);

            if (this.appSettings.SelectedCluster != null)
            {
                // Selected cluster.
                pageType = typeof(ClusterMasterDetailPage);
            }

            return NavigationService.InternalNavigate(pageType);
        }

        public Task NavigateToAddAccountPage() => NavigationService.InternalNavigate(typeof(AddAccountPage));

        public Task NavigateToAddEditAzureAccountPage(string id = null) => NavigationService.InternalNavigate(typeof(AddEditAzureAccountPage), id);

        public async Task NavigateToClusterOverviewPage()
        {
            if (Application.Current.MainPage is ClusterMasterDetailPage masterDetailPage)
            {
                ExtendedNavigationPage detail = (ExtendedNavigationPage)masterDetailPage.Detail;
                await detail.PopToRootAsync();
            }
            else
            {
                await NavigationService.InternalNavigate(typeof(ClusterMasterDetailPage));

                await this.RemoveBackStack();
            }
        }

        public async Task GoBackToClustersPage()
        {
            if (Application.Current.MainPage is ExtendedNavigationPage mainPage)
            {
                await mainPage.PopToRootAsync();
            }
        }

        public Task NavigateToClustersPage() => NavigationService.InternalNavigate(typeof(ClustersPage));

        public Task NavigateToPodsPage() => NavigationService.InternalNavigate(typeof(PodsPage));

        public Task NavigateToPodDetailPage(string name, string namespaceName) => NavigationService.InternalNavigate(typeof(PodDetailPage), new ObjectId(name, namespaceName));

        public Task NavigateToDeploymentsPage() => NavigationService.InternalNavigate(typeof(DeploymentsPage));

        public Task NavigateToDeploymentDetailPage(string name, string namespaceName) => NavigationService.InternalNavigate(typeof(DeploymentDetailPage), new ObjectId(name, namespaceName));

        public Task NavigateToReplicaSetsPage() => NavigationService.InternalNavigate(typeof(ReplicaSetsPage));

        public Task NavigateToReplicaSetDetailPage(string name, string namespaceName) => NavigationService.InternalNavigate(typeof(ReplicaSetDetailPage), new ObjectId(name, namespaceName));

        public Task NavigateToServicesPage() => NavigationService.InternalNavigate(typeof(ServicesPage));

        public Task NavigateToServiceDetailPage(string name, string namespaceName) => NavigationService.InternalNavigate(typeof(ServiceDetailPage), new ObjectId(name, namespaceName));

        public Task NavigateToIngressesPage() => NavigationService.InternalNavigate(typeof(IngressesPage));

        public Task NavigateToConfigMapsPage() => NavigationService.InternalNavigate(typeof(ConfigMapsPage));

        public Task NavigateToSecretsPage() => NavigationService.InternalNavigate(typeof(SecretsPage));

        public Task NavigateToCronJobsPage() => NavigationService.InternalNavigate(typeof(CronJobsPage));

        public Task NavigateToCronJobDetailPage(string name, string namespaceName) => NavigationService.InternalNavigate(typeof(CronJobDetailPage), new ObjectId(name, namespaceName));

        public Task NavigateToDaemonSetsPage() => NavigationService.InternalNavigate(typeof(DaemonSetsPage));

        public Task NavigateToDaemonSetDetailPage(string name, string namespaceName) => NavigationService.InternalNavigate(typeof(DaemonSetDetailPage), new ObjectId(name, namespaceName));

        public Task NavigateToJobsPage() => NavigationService.InternalNavigate(typeof(JobsPage));

        public Task NavigateToReplicationControllersPage() => NavigationService.InternalNavigate(typeof(ReplicationControllersPage));

        public Task NavigateToReplicationControllerDetailPage(string name, string namespaceName) => NavigationService.InternalNavigate(typeof(ReplicationControllerDetailPage), new ObjectId(name, namespaceName));

        public Task NavigateToPersistentVolumeClaimsPage() => NavigationService.InternalNavigate(typeof(PersistentVolumeClaimsPage));

        public Task NavigateToStatefulSetsPage() => NavigationService.InternalNavigate(typeof(StatefulSetsPage));

        public Task NavigateToSettingsPage() => NavigationService.InternalNavigate(typeof(SettingsPage));

        public Task NavigateToResourceListingPage() => NavigationService.InternalNavigate(typeof(ResourceListingPage));

        public Task RemoveLastFromBackStack()
        {
            if (Application.Current.MainPage is ExtendedNavigationPage mainPage)
            {
                mainPage.Navigation.RemovePage(
                    mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2]);
            }

            return Task.CompletedTask;
        }

        public Task RemoveBackStack()
        {
            if (Application.Current.MainPage is ExtendedNavigationPage mainPage)
            {
                for (int i = 0; i < mainPage.Navigation.NavigationStack.Count - 1; i++)
                {
                    Page page = mainPage.Navigation.NavigationStack[i];
                    mainPage.Navigation.RemovePage(page);
                }
            }

            return Task.CompletedTask;
        }

        private static async Task InternalNavigate(Type pageType, object parameter = null)
        {
            if (NavigationService.IsCurrentlyOnPage(pageType))
            {
                return;
            }

            // TODO Performance improvement by caching pages ??
            // See https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/navigation#handling-navigation-requests for more information.
            Page page = Activator.CreateInstance(pageType) as Page;

            switch (Application.Current.MainPage)
            {
                case ClusterMasterDetailPage masterDetailPage:
                    if (page is ClustersPage)
                    {
                        // Resetting the stack.
                        Application.Current.MainPage = new ExtendedNavigationPage(page);
                    }
                    else
                    {
                        await ((ExtendedNavigationPage)masterDetailPage.Detail).PushAsync(page);
                    }

                    break;
                case ExtendedNavigationPage navigationPage:
                    if (page is ClusterMasterDetailPage)
                    {
                        Application.Current.MainPage = page;
                    }
                    else
                    {
                        await navigationPage.PushAsync(page);
                    }

                    break;
                default:
                    Application.Current.MainPage = page is ClusterMasterDetailPage ? page : new ExtendedNavigationPage(page);
                    break;
            }

            if (page.BindingContext is ViewModelBase viewModel)
            {
                await viewModel.Initialize(parameter);
            }
        }

        private static bool IsCurrentlyOnPage(Type pageType)
        {
            Page mainPage = Application.Current.MainPage;

            if (mainPage == null)
            {
                return false;
            }

            if (mainPage is ClusterMasterDetailPage masterDetailPage &&
                masterDetailPage.Detail is ExtendedNavigationPage detailPage &&
                detailPage.CurrentPage.GetType() == pageType)
            {
                return true;
            }

            if (mainPage is ExtendedNavigationPage navigationPage &&
                navigationPage.CurrentPage.GetType() == pageType)
            {
                return true;
            }

            return false;
        }
    }
}