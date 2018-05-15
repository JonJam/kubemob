using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using k8s;
using k8s.Models;
using KubeMob.Common;
using KubeMob.Common.Exceptions;
using KubeMob.Common.Services.AccountManagement;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Settings;
using Xamarin.Forms.Internals;

[assembly: Xamarin.Forms.Dependency(typeof(KubeMob.iOS.Services.Kubernetes.KubernetesService))]
namespace KubeMob.iOS.Services.Kubernetes
{
    /// <summary>
    /// Created platform specific implementation as <see cref="k8s.Kubernetes"/> needs to perform platform specific exception handling.
    ///
    /// Microsoft.Rest.ServiceClient which <see cref="k8s.Kubernetes"/> uses has built in retry logic: https://github.com/Azure/autorest/blob/master/docs/client/retry.md.
    /// </summary>
    public class KubernetesService : KubernetesServiceBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KubernetesService"/> class.
        /// 
        /// Since this is being constructed using <see cref="Xamarin.Forms.DependencyService"/> it has to be parameterless.
        /// Therefore using the ViewModelLocator to look up the dependencies.
        /// </summary>
        [Preserve]
        public KubernetesService()
            : base(ViewModelLocator.Resolve<IAppSettings>(), ViewModelLocator.Resolve<IEnumerable<IAccountManager>>())
        {
        }

        [Preserve]
        public KubernetesService(
            IAppSettings appSettings,
            IEnumerable<IAccountManager> accountManagers)
            : base(appSettings, accountManagers)
        {
        }

        protected override IKubernetes ConfigureClientForPlatform(k8s.Kubernetes client) => client;

        // TODO Refactor below

        protected override async Task<V1DeploymentList> GetDeployments()
        {
            try
            {
                IKubernetes client = await this.Client.Value;

                return await client.ListDeploymentForAllNamespacesAsync();
            }
            catch (HttpRequestException e) when (e.InnerException is WebException web &&
                                                 web.Status == WebExceptionStatus.NameResolutionFailure)
            {
                // TODO Verify this is correct exception type on device.
                // No internet.
                throw new NoNetworkException(e.Message, e);
            }
        }

        protected override async Task<V1PodList> GetPods()
        {
            try
            {
                IKubernetes client = await this.Client.Value;

                return await client.ListPodForAllNamespacesAsync();
            }
            catch (HttpRequestException e) when (e.InnerException is WebException web &&
                                                web.Status == WebExceptionStatus.NameResolutionFailure)
            {
                // TODO Verify this is correct exception type on device.
                // No internet.
                throw new NoNetworkException(e.Message, e);
            }
        }

        protected override async Task<V1ReplicaSetList> GetReplicaSets()
        {
            try
            {
                IKubernetes client = await this.Client.Value;

                return await client.ListReplicaSetForAllNamespacesAsync();
            }
            catch (HttpRequestException e) when (e.InnerException is WebException web &&
                                                 web.Status == WebExceptionStatus.NameResolutionFailure)
            {
                // TODO Verify this is correct exception type on device.
                // No internet.
                throw new NoNetworkException(e.Message, e);
            }
        }

        protected override async Task<V1ServiceList> GetServices()
        {
            try
            {
                IKubernetes client = await this.Client.Value;

                return await client.ListServiceForAllNamespacesAsync();
            }
            catch (HttpRequestException e) when (e.InnerException is WebException web &&
                                                 web.Status == WebExceptionStatus.NameResolutionFailure)
            {
                // TODO Verify this is correct exception type on device.
                // No internet.
                throw new NoNetworkException(e.Message, e);
            }
        }

        protected override async Task<V1beta1IngressList> GetIngresses()
        {
            try
            {
                IKubernetes client = await this.Client.Value;

                return await client.ListIngressForAllNamespacesAsync();
            }
            catch (HttpRequestException e) when (e.InnerException is WebException web &&
                                                 web.Status == WebExceptionStatus.NameResolutionFailure)
            {
                // TODO Verify this is correct exception type on device.
                // No internet.
                throw new NoNetworkException(e.Message, e);
            }
        }
        
        protected override async Task<V1ConfigMapList> GetConfigMaps()
        {
            try
            {
                IKubernetes client = await this.Client.Value;

                return await client.ListConfigMapForAllNamespacesAsync();
            }
            catch (HttpRequestException e) when (e.InnerException is WebException web &&
                                                 web.Status == WebExceptionStatus.NameResolutionFailure)
            {
                // TODO Verify this is correct exception type on device.
                // No internet.
                throw new NoNetworkException(e.Message, e);
            }
        }

        protected override async Task<V1SecretList> GetSecrets()
        {
            try
            {
                IKubernetes client = await this.Client.Value;

                return await client.ListSecretForAllNamespacesAsync();
            }
            catch (HttpRequestException e) when (e.InnerException is WebException web &&
                                                 web.Status == WebExceptionStatus.NameResolutionFailure)
            {
                // TODO Verify this is correct exception type on device.
                // No internet.
                throw new NoNetworkException(e.Message, e);
            }
        }

        protected override async Task<V1beta1CronJobList> GetCronJobs()
        {
            try
            {
                IKubernetes client = await this.Client.Value;

                return await client.ListCronJobForAllNamespacesAsync();
            }
            catch (HttpRequestException e) when (e.InnerException is WebException web &&
                                                 web.Status == WebExceptionStatus.NameResolutionFailure)
            {
                // TODO Verify this is correct exception type on device.
                // No internet.
                throw new NoNetworkException(e.Message, e);
            }
        }

        protected override async Task<V1DaemonSetList> GetDaemonSets()
        {
            try
            {
                IKubernetes client = await this.Client.Value;

                return await client.ListDaemonSetForAllNamespacesAsync();
            }
            catch (HttpRequestException e) when (e.InnerException is WebException web &&
                                                 web.Status == WebExceptionStatus.NameResolutionFailure)
            {
                // TODO Verify this is correct exception type on device.
                // No internet.
                throw new NoNetworkException(e.Message, e);
            }
        }

        protected override async Task<V1JobList> GetJobs()
        {
            try
            {
                IKubernetes client = await this.Client.Value;

                return await client.ListJobForAllNamespacesAsync();
            }
            catch (HttpRequestException e) when (e.InnerException is WebException web &&
                                                 web.Status == WebExceptionStatus.NameResolutionFailure)
            {
                // TODO Verify this is correct exception type on device.
                // No internet.
                throw new NoNetworkException(e.Message, e);
            }
        }
    }
}
