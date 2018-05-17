using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using AutoMapper;
using KubeMob.Common.Services.AccountManagement;
using KubeMob.Common.Services.AccountManagement.Azure;
using KubeMob.Common.Services.AccountManagement.Azure.Model;
using KubeMob.Common.Services.AccountManagement.Model;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Localization;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Services.Popup;
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
            serviceCollection.AddTransient<DeploymentsViewModel>();
            serviceCollection.AddTransient<ReplicaSetsViewModel>();
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
                cfg.CreateMap<CloudAccount, Account>()
                    .ConstructUsing((ca) => new Account(ca.Id, ca.Properties));

                cfg.CreateMap<Account, AzureAccount>()
                    .ConstructUsing((a) => new AzureAccount(a.Username, a.Properties));

                cfg.CreateMap<k8s.Models.V1Deployment, DeploymentSummary>()
                    .ConstructUsing((d) => new DeploymentSummary(
                        d.Metadata.Name,
                        $"{d.Status.AvailableReplicas}/{d.Status.Replicas}"));

                cfg.CreateMap<k8s.Models.V1Pod, PodSummary>()
                    .ConstructUsing((p) => new PodSummary(p.Metadata.Name, p.Status.Phase));

                cfg.CreateMap<k8s.Models.V1ReplicaSet, ReplicaSetSummary>()
                    .ConstructUsing((r) => new ReplicaSetSummary(
                        r.Metadata.Name,
                        $"{r.Status.AvailableReplicas.GetValueOrDefault(0)}/{r.Status.Replicas}"));

                cfg.CreateMap<k8s.Models.V1Service, ServiceSummary>()
                    .ConstructUsing((r) => new ServiceSummary(
                        r.Metadata.Name,
                        r.Spec.ClusterIP));

                cfg.CreateMap<k8s.Models.V1beta1Ingress, IngressSummary>()
                    .ConstructUsing((r) => new IngressSummary(
                        r.Metadata.Name));

                cfg.CreateMap<k8s.Models.V1ConfigMap, ConfigMapSummary>()
                    .ConstructUsing((r) => new ConfigMapSummary(
                        r.Metadata.Name));

                cfg.CreateMap<k8s.Models.V1Secret, SecretSummary>()
                    .ConstructUsing((r) => new SecretSummary(
                        r.Metadata.Name,
                        r.Type));

                cfg.CreateMap<k8s.Models.V1beta1CronJob, CronJobSummary>()
                    .ConstructUsing((r) => new CronJobSummary(
                        r.Metadata.Name,
                        r.Spec.Schedule));

                cfg.CreateMap<k8s.Models.V1DaemonSet, DaemonSetSummary>()
                    .ConstructUsing((r) => new DaemonSetSummary(
                        r.Metadata.Name,
                        $"{r.Status.CurrentNumberScheduled}/{r.Status.DesiredNumberScheduled}"));

                cfg.CreateMap<k8s.Models.V1Job, JobSummary>()
                    .ConstructUsing((r) => new JobSummary(
                        r.Metadata.Name,
                        $"{r.Status.Active.GetValueOrDefault(0)}/{r.Spec.Parallelism}"));

                cfg.CreateMap<k8s.Models.V1ReplicationController, ReplicationControllerSummary>()
                    .ConstructUsing((r) => new ReplicationControllerSummary(
                        r.Metadata.Name,
                        $"{r.Status.AvailableReplicas.GetValueOrDefault(0)}/{r.Spec.Replicas}"));

                cfg.CreateMap<k8s.Models.V1StatefulSet, StatefulSetSummary>()
                    .ConstructUsing((r) => new StatefulSetSummary(
                        r.Metadata.Name,
                        $"{r.Status.CurrentReplicas.GetValueOrDefault(0)}/{r.Status.Replicas}"));

                cfg.CreateMap<k8s.Models.V1PersistentVolumeClaim, PersistentVolumeClaimsSummary>()
                    .ConstructUsing((r) => new PersistentVolumeClaimsSummary(
                        r.Metadata.Name,
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
