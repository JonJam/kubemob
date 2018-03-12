using System;
using System.Globalization;
using System.Reflection;
using KubeMob.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace KubeMob
{
    // Based off <see cref="https://developer.xamarin.com/guides/xamarin-forms/enterprise-application-patterns/mvvm/#Automatically_Creating_a_View_Model_with_a_View_Model_Locator"/>
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
            propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(ViewModelLocator.AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(ViewModelLocator.AutoWireViewModelProperty, value);
        }

        // TODO Change to void
        private static IServiceCollection ConfigureViewModels(
            IServiceCollection serviceCollection)
        {
            // TODO Add ViewModels
            // TODO Implement design time ? Whether need to?
            serviceCollection.AddTransient<MainViewModel>();

            return serviceCollection;
        }

        // TODO Change to void
        private static IServiceCollection ConfigureServices(IServiceCollection serviceCollection)
        {
            // TODO Add Services;

            return serviceCollection;
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;
            if (view == null)
            {
                return;
            }

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Pages.", ".ViewModels.");

            viewName = viewName.Replace("Page", "");

            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}ViewModel, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }
            var viewModel = ViewModelLocator.serviceProvider.GetService(viewModelType);
            view.BindingContext = viewModel;
        }
    }
}
