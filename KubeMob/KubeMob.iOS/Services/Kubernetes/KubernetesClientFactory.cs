using k8s;
using KubeMob.Common.Services.Kubernetes;
using Xamarin.Forms.Internals;

[assembly: Xamarin.Forms.Dependency(typeof(KubeMob.iOS.Services.Kubernetes.KubernetesClientFactory))]
namespace KubeMob.iOS.Services.Kubernetes
{
    /// <summary>
    /// Created platform specific implementation as <see cref="k8s.Kubernetes"/> needs to perform platform specific initialization. If we did this in the KubeMob.Common project, it will utitlize .NET Standard APIs
    /// and fail due to limitations of Mono implementation for HttpClientHandler.
    /// </summary>
    public class KubernetesClientFactory : IKubernetesClientFactory
    {
        [Preserve]
        public KubernetesClientFactory()
        {
        }

        public IKubernetes CreateClient(KubernetesClientConfiguration config) => new k8s.Kubernetes(config);
    }
}
