using System.Collections.Generic;
using System.Threading.Tasks;
using k8s;
using k8s.Models;
using KubeMob.Common.Exceptions;
using KubeMob.Common.Services.AccountManagement;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Settings;
using Xamarin.Forms.Internals;

[assembly: Xamarin.Forms.Dependency(typeof(KubeMob.Droid.Services.Kubernetes.KubernetesService))]
namespace KubeMob.Droid.Services.Kubernetes
{
    /// <summary>
    /// Created platform specific implementation as <see cref="k8s.Kubernetes"/> needs to perform platform specific initialization. If we did this in the KubeMob.Common project, it will utitlize .NET Standard APIs
    /// and fail due to limitations of Mono implementation for HttpClientHandler.
    /// </summary>
    public class KubernetesService : KubernetesServiceBase
    {
        [Preserve]
        public KubernetesService(
            IAppSettings appSettings,
            IEnumerable<IAccountManager> accountManagers)
            : base(appSettings, accountManagers)
        {
        }

        protected override async Task<V1PodList> GetPods()
        {
            try
            {
                IKubernetes client = await this.Client.Value;

                return await client.ListPodForAllNamespacesAsync();
            }
            catch (Java.Net.UnknownHostException e)
            {
                // No internet.
                throw new NoNetworkException(e.Message, e);
            }
        }
    }
}
