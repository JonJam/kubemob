﻿using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace KubeMob.Common
{
    /// <summary>
    /// Based off <see cref="https://developer.xamarin.com/guides/xamarin-forms/enterprise-application-patterns/mvvm/#Automatically_Creating_a_View_Model_with_a_View_Model_Locator"/>
    /// </summary>
    public static class ViewModelLocator
    {
        private static ServiceProvider serviceProvider;

        static ViewModelLocator()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            ViewModelLocator.ConfigureViewModels(serviceCollection);
            ViewModelLocator.ConfigureServices(serviceCollection);

            ViewModelLocator.serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public static readonly BindableProperty AutoWireViewModelProperty = BindableProperty.CreateAttached(
            "AutoWireViewModel",
            typeof(bool),
            typeof(ViewModelLocator),
            default(bool),
            propertyChanged: ViewModelLocator.OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable) => (bool)bindable.GetValue(ViewModelLocator.AutoWireViewModelProperty);

        public static void SetAutoWireViewModel(BindableObject bindable, bool value) => bindable.SetValue(ViewModelLocator.AutoWireViewModelProperty, value);

        public static T Resolve<T>() where T : class => ViewModelLocator.serviceProvider.GetService<T>();

        private static void ConfigureViewModels(
            IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<MainViewModel>();
            serviceCollection.AddTransient<OtherViewModel>();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<INavigationService, NavigationService>();

            serviceCollection.AddSingleton((sp) => new ResourceManager("KubeMob.Common.Resx.AppResources", typeof(ViewModelLocator).GetTypeInfo().Assembly));
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is Element view))
            {
                return;
            }

            Type viewType = view.GetType();
            string viewName = viewType.FullName.Replace(".Pages.", ".ViewModels.");

            viewName = viewName.Replace("Page", "");

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