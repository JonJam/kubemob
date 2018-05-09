using System.Collections.Generic;
using System.Threading.Tasks;

namespace KubeMob.Common.Services.Kubernetes
{
    public interface IKubernetesService
    {
        Task<IList<PodSummary>> GetPodSummaries();
    }
}
