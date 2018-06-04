using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using AutoMapper;
using KubeMob.Common.Services.AccountManagement;
using KubeMob.Common.Services.AccountManagement.Azure;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.MappingProfiles;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Localization;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.Services.PubSub;
using KubeMob.Common.Services.Settings;
using KubeMob.Common.ViewModels;
using KubeMob.Common.ViewModels.Deployments;
using KubeMob.Common.ViewModels.MasterDetail;
using KubeMob.Common.ViewModels.Pods;
using KubeMob.Common.ViewModels.ReplicaSets;
using KubeMob.Common.ViewModels.Settings;
using Microsoft.Extensions.DependencyInjection;
using Plugin.Settings;
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

            serviceCollection.AddTransient<ClusterMasterDetailViewModel>();
            serviceCollection.AddTransient<ClusterMasterViewModel>();

            serviceCollection.AddTransient<ClusterOverviewViewModel>();

            serviceCollection.AddTransient<PodsViewModel>();
            serviceCollection.AddTransient<PodDetailViewModel>();

            serviceCollection.AddTransient<DeploymentsViewModel>();
            serviceCollection.AddTransient<DeploymentDetailViewModel>();

            serviceCollection.AddTransient<ReplicaSetsViewModel>();
            serviceCollection.AddTransient<ReplicaSetDetailViewModel>();

            serviceCollection.AddTransient<ServicesViewModel>();
            serviceCollection.AddTransient<IngressesViewModel>();
            serviceCollection.AddTransient<ConfigMapsViewModel>();
            serviceCollection.AddTransient<SecretsViewModel>();
            serviceCollection.AddTransient<CronJobsViewModel>();
            serviceCollection.AddTransient<DaemonSetsViewModel>();
            serviceCollection.AddTransient<JobsViewModel>();
            serviceCollection.AddTransient<ReplicationControllersViewModel>();
            serviceCollection.AddTransient<PersistentVolumeClaimsViewModel>();
            serviceCollection.AddTransient<StatefulSetsViewModel>();

            serviceCollection.AddTransient<SettingsViewModel>();
            serviceCollection.AddTransient<ResourceListingViewModel>();
        }

        private static void ConfigureXamPlugins(IServiceCollection serviceCollection) => serviceCollection.AddSingleton(CrossSettings.Current);

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IAzureAccountManager, AzureAccountManager>();
            serviceCollection.AddSingleton<IAccountManager, AzureAccountManager>();

            serviceCollection.AddSingleton<IPubSubService, PubSubService>();
            serviceCollection.AddSingleton<INavigationService, NavigationService>();
            serviceCollection.AddSingleton<IPopupService, PopupService>();
            serviceCollection.AddSingleton<IAppSettings, AppSettings>();
            serviceCollection.AddSingleton((sp) => DependencyService.Get<ILocalize>());

            serviceCollection.AddSingleton((sp) => DependencyService.Get<IKubernetesService>());

            serviceCollection.AddSingleton((sp) => new ResourceManager("KubeMob.Common.Resx.AppResources", typeof(ViewModelLocator).GetTypeInfo().Assembly));
        }

        private static void ConfigureMaps() =>

            // Using AutoMapper 6.1.1 rather than latest due to build error which is detailled here: https://github.com/AutoMapper/AutoMapper/issues/2455
            // Linker configured to skip the following otherwise causes this to fail:
            // - AutoMapper
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<DeploymentMappingProfile>();
                cfg.AddProfile<PodMappingProfile>();
                cfg.AddProfile<ReplicaSetMappingProfile>();

                cfg.CreateMap<k8s.Models.V1Service, ObjectSummary>()
                    .ConstructUsing((r) => new ObjectSummary(
                        r.Metadata.Name,
                        r.Metadata.NamespaceProperty,
                        r.Spec.ClusterIP));

                cfg.CreateMap<k8s.Models.V1beta1Ingress, ObjectSummary>()
                    .ConstructUsing((r) => new ObjectSummary(
                        r.Metadata.Name,
                        r.Metadata.NamespaceProperty));

                cfg.CreateMap<k8s.Models.V1ConfigMap, ObjectSummary>()
                    .ConstructUsing((r) => new ObjectSummary(
                        r.Metadata.Name,
                        r.Metadata.NamespaceProperty));

                cfg.CreateMap<k8s.Models.V1Secret, ObjectSummary>()
                    .ConstructUsing((r) => new ObjectSummary(
                        r.Metadata.Name,
                        r.Metadata.NamespaceProperty,
                        r.Type));

                cfg.CreateMap<k8s.Models.V1beta1CronJob, ObjectSummary>()
                    .ConstructUsing((r) => new ObjectSummary(
                        r.Metadata.Name,
                        r.Metadata.NamespaceProperty,
                        r.Spec.Schedule));

                cfg.CreateMap<k8s.Models.V1DaemonSet, ObjectSummary>()
                    .ConstructUsing((r) => new ObjectSummary(
                        r.Metadata.Name,
                        r.Metadata.NamespaceProperty,
                        $"{r.Status.CurrentNumberScheduled}/{r.Status.DesiredNumberScheduled}"));

                cfg.CreateMap<k8s.Models.V1Job, ObjectSummary>()
                    .ConstructUsing((r) => new ObjectSummary(
                        r.Metadata.Name,
                        r.Metadata.NamespaceProperty,
                        $"{r.Status.Active.GetValueOrDefault(0)}/{r.Spec.Parallelism}"));

                cfg.CreateMap<k8s.Models.V1ReplicationController, ObjectSummary>()
                    .ConstructUsing((r) => new ObjectSummary(
                        r.Metadata.Name,
                        r.Metadata.NamespaceProperty,
                        $"{r.Status.AvailableReplicas.GetValueOrDefault(0)}/{r.Spec.Replicas}"));

                cfg.CreateMap<k8s.Models.V1StatefulSet, ObjectSummary>()
                    .ConstructUsing((r) => new ObjectSummary(
                        r.Metadata.NamespaceProperty,
                        r.Metadata.Name,
                        $"{r.Status.CurrentReplicas.GetValueOrDefault(0)}/{r.Status.Replicas}"));

                cfg.CreateMap<k8s.Models.V1PersistentVolumeClaim, ObjectSummary>()
                    .ConstructUsing((r) => new ObjectSummary(
                        r.Metadata.Name,
                        r.Metadata.NamespaceProperty,
                        r.Status.Phase));
            });

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
