using System.Threading.Tasks;

namespace KubeMob.Common.Services.Kubernetes
{
    public interface IKubernetesService
    {
        Task GetPodsSummary();
    }
}
