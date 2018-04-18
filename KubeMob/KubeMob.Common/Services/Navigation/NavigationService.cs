using System;
using System.Linq;
using System.Threading.Tasks;
using KubeMob.Common.Pages;
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
            // TODO Test
            Type pageType = typeof(ClustersPage);

            if (!this.appSettings.GetAzureAccounts().Any())
            {
                // No accounts.
                pageType = typeof(AddAccountPage);
            }
            else if (this.appSettings.SelectedCluster != null)
            {
                // Selected cluster.
                pageType = typeof(ClustersPage);
            }

            return NavigationService.InternalNavigate(pageType);
        }

        public Task NavigateToAddAccountPage() => NavigationService.InternalNavigate(typeof(AddAccountPage));

        public Task NavigateToAddEditAzureAccountPage(string id = null) => NavigationService.InternalNavigate(typeof(AddEditAzureAccountPage), id);

        public async Task NavigateToClusterPage()
        {
            await NavigationService.InternalNavigate(typeof(ClusterMasterDetailPage));

            await this.RemoveBackStack();
        }

        public async Task GoBackToClusterPage()
        {
            if (Application.Current.MainPage is ExtendedNavigationPage mainPage)
            {
                do
                {
                    await mainPage.PopAsync();
                }
                while (mainPage.CurrentPage.GetType() != typeof(ClustersPage));
            }
        }

        public Task NavigateToPodsPage() => NavigationService.InternalNavigate(typeof(PodsPage));

        public Task NavigateToPodDetailPage() => NavigationService.InternalNavigate(typeof(PodDetailsPage));

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

            // TODO Performance improvement by caching pages.
            // See https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/navigation#handling-navigation-requests for more information.
            Page page = Activator.CreateInstance(pageType) as Page;

            if (Application.Current.MainPage is ClusterMasterDetailPage masterDetailPage)
            {
                await (masterDetailPage.Detail as ExtendedNavigationPage).PushAsync(page);
            }
            else if (Application.Current.MainPage is ExtendedNavigationPage navigationPage)
            {
                if (page is ClusterMasterDetailPage)
                {
                    Application.Current.MainPage = page;
                }
                else
                {
                    await navigationPage.PushAsync(page);
                }
            }
            else
            {
                Application.Current.MainPage = new ExtendedNavigationPage(page);
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
