﻿using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using AutoMapper;
using KubeMob.Common.Services.AccountManagement;
using KubeMob.Common.Services.AccountManagement.Azure;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Services.Settings;
using KubeMob.Common.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Plugin.Settings;
using Xamarin.Auth;
using Xamarin.Forms;

namespace KubeMob.Common
{
    /// <summary>
    /// Based off <see cref="https://developer.xamarin.com/guides/xamarin-forms/enterprise-application-patterns/mvvm/#Automatically_Creating_a_View_Model_with_a_View_Model_Locator"/>
    /// </summary>
    public static class ViewModelLocator
    {
        public static readonly BindableProperty AutoWireViewModelProperty = BindableProperty.CreateAttached(
            "AutoWireViewModel",
            typeof(bool),
            typeof(ViewModelLocator),
            default(bool),
            propertyChanged: ViewModelLocator.OnAutoWireViewModelChanged);

        private static ServiceProvider serviceProvider;

        static ViewModelLocator()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            ViewModelLocator.ConfigureViewModels(serviceCollection);
            ViewModelLocator.ConfigureXamPlugins(serviceCollection);
            ViewModelLocator.ConfigureServices(serviceCollection);
            ViewModelLocator.ConfigureMaps();

            ViewModelLocator.serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public static bool GetAutoWireViewModel(BindableObject bindable) => (bool)bindable.GetValue(ViewModelLocator.AutoWireViewModelProperty);

        public static void SetAutoWireViewModel(BindableObject bindable, bool value) => bindable.SetValue(ViewModelLocator.AutoWireViewModelProperty, value);

        public static T Resolve<T>()
            where T : class
            => ViewModelLocator.serviceProvider.GetService<T>();

        private static void ConfigureViewModels(
            IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ClustersViewModel>();
            serviceCollection.AddTransient<AddAccountViewModel>();
            serviceCollection.AddTransient<AddEditAzureAccountViewModel>();

            serviceCollection.AddTransient<ClusterOverviewViewModel>();
            serviceCollection.AddTransient<ClusterMasterViewModel>();
            serviceCollection.AddTransient<PodsViewModel>();
        }

        private static void ConfigureXamPlugins(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(CrossSettings.Current);
            serviceCollection.AddSingleton(AccountStore.Create());
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IAzureAccountManager, AzureAccountManager>();
            serviceCollection.AddSingleton<IAccountManager, AzureAccountManager>();

            serviceCollection.AddSingleton<IKubernetesService, KubernetesService>();

            serviceCollection.AddSingleton<INavigationService, NavigationService>();
            serviceCollection.AddSingleton<IAppSettings, AppSettings>();

            serviceCollection.AddSingleton((sp) => new ResourceManager("KubeMob.Common.Resx.AppResources", typeof(ViewModelLocator).GetTypeInfo().Assembly));
        }

        private static void ConfigureMaps()
        {
            // Using AutoMapper 6.1.1 rather than latest due to build error which is detailled here: https://github.com/AutoMapper/AutoMapper/issues/2455
            // Linker configured to skip the following otherwise causes this to fail:
            // - AutoMapper
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CloudAccount, Account>()
                    .ConstructUsing((ca) => new Account(ca.Id, ca.Properties));

                cfg.CreateMap<Account, AzureAccount>()
                    .ConstructUsing((a) => new AzureAccount(a.Username, a.Properties));
            });
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is Element view))
            {
                return;
            }

            Type viewType = view.GetType();
            string viewName = viewType.FullName.Replace(".Pages.", ".ViewModels.");

            viewName = viewName.Replace("Page", string.Empty);

            string viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            string viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}ViewModel, {1}", viewName, viewAssemblyName);

            Type viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }

            object viewModel = ViewModelLocator.serviceProvider.GetService(viewModelType);
            view.BindingContext = viewModel;
        }
    }
}
