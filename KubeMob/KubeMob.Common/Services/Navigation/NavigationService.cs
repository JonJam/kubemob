﻿using System;
using System.Threading.Tasks;
using KubeMob.Common.Pages;
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
        [Preserve]
        public NavigationService()
        {
        }

        public Task Initialize()
        {
            // TODO Check if selected cluster previously, then navigate to cluster page.
            return NavigationService.InternalNavigate(typeof(ClustersPage));
        }

        public Task NavigateToAddClusterPage() => NavigationService.InternalNavigate(typeof(AddClusterPage));

        public Task NavigateToClusterPage() => NavigationService.InternalNavigate(typeof(ClusterMasterDetailPage));

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

        public Task GoBack()
        {
            if (Application.Current.MainPage is ExtendedNavigationPage mainPage)
            {
                mainPage.PopAsync();
            }

            return Task.CompletedTask;
        }

        private static async Task InternalNavigate(Type pageType, object parameter = null)
        {
            if (Application.Current.MainPage != null &&
                Application.Current.MainPage.GetType() == pageType)
            {
                // Ensuring not re-navigating to same page already on.
                return;
            }

            // TODO Performance improvement by caching pages.
            // See https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/navigation#handling-navigation-requests for more information.
            Page page = Activator.CreateInstance(pageType) as Page;

            if (Application.Current.MainPage is ClusterMasterDetailPage masterDetailPage)
            {
                await (masterDetailPage.Detail as NavigationPage).PushAsync(page);
            }
            else if (Application.Current.MainPage is ExtendedNavigationPage navigationPage)
            {
                if (page is ClusterMasterDetailPage clusterPage)
                {
                    // Initialising the detail and master page here in order to use DI.
                    clusterPage.Master = Activator.CreateInstance(typeof(ClusterMasterPage)) as Page;
                    clusterPage.Detail = new ExtendedNavigationPage(Activator.CreateInstance(typeof(ClusterOverviewPage)) as Page);

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
    }
}
