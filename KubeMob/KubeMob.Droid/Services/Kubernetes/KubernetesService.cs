using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using k8s;
using KubeMob.Common;
using KubeMob.Common.Exceptions;
using KubeMob.Common.Services.AccountManagement;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.PubSub;
using KubeMob.Common.Services.Settings;
using Microsoft.Rest;
using Microsoft.Rest.TransientFaultHandling;
using Xamarin.Forms.Internals;

[assembly: Xamarin.Forms.Dependency(typeof(KubeMob.Droid.Services.Kubernetes.KubernetesService))]
namespace KubeMob.Droid.Services.Kubernetes
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
            : base(ViewModelLocator.Resolve<IAppSettings>(), ViewModelLocator.Resolve<IPubSubService>(), ViewModelLocator.Resolve<IEnumerable<IAccountManager>>())
        {
        }

        [Preserve]
        public KubernetesService(
            IAppSettings appSettings,
            IPubSubService pubSubService,
            IEnumerable<IAccountManager> accountManagers)
            : base(appSettings, pubSubService, accountManagers)
        {
        }

        protected override IKubernetes ConfigureClientForPlatform(k8s.Kubernetes client)
        {
            RetryPolicy<KubernetesTransientErrorDetectionStrategy> policy = new RetryPolicy<KubernetesTransientErrorDetectionStrategy>(new ExponentialBackoffRetryStrategy());

            client.SetRetryPolicy(policy);

            return client;
        }

        protected override async Task<T> PerformClientOperation<T>(Func<IKubernetes, Task<T>> clientOperation)
        {
            try
            {
                IKubernetes client = await this.Client.Value;

                return await clientOperation(client);
            }
            catch (Java.Net.UnknownHostException e)
            {
                // No internet.
                throw new NoNetworkException(e.Message, e);
            }
            catch (OperationCanceledException e)
            {
                // Request was cancelled e.g. a Kubernetes cluster being brought up and not responding
                // to API calls. Treat as no internet.
                throw new NoNetworkException(e.Message, e);
            }
            catch (HttpOperationException e) when (e.Response.StatusCode == HttpStatusCode.NotFound)
            {
                // The Kubernetes cluster doesn't support the API we are using.
                throw new ObjectTypeNotSupportedException(e.Message, e);
            }
        }
    }
}
