using System.Collections.Generic;
using System.Threading.Tasks;

namespace KubeMob.Common.Services.Kubernetes
{
    public interface IKubernetesService
    {
        Task<IList<DeploymentSummary>> GetDeploymentSummaries();

        Task<IList<PodSummary>> GetPodSummaries();

        Task<IList<ReplicaSetSummary>> GetReplicaSetSummaries();
    }
}
