using k8s;

namespace KubeMob.Common.Services.Kubernetes
{
    public interface IKubernetesClientFactory
    {
        k8s.IKubernetes CreateClient(KubernetesClientConfiguration config);
    }
}
