using System.Collections.Generic;
using System.Threading.Tasks;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes
{
    public interface IKubernetesService
    {
        Task<IList<ObjectSummary>> GetDeploymentSummaries();

        Task<IList<ObjectSummary>> GetPodSummaries();

        Task<IList<ObjectSummary>> GetReplicaSetSummaries();

        Task<IList<ObjectSummary>> GetServiceSummaries();

        Task<IList<ObjectSummary>> GetIngressSummaries();

        Task<IList<ObjectSummary>> GetConfigMapSummaries();

        Task<IList<ObjectSummary>> GetSecretSummaries();

        Task<IList<ObjectSummary>> GetCronJobSummaries();

        Task<IList<ObjectSummary>> GetDaemonSetSummaries();

        Task<IList<ObjectSummary>> GetJobSummaries();

        Task<IList<ObjectSummary>> GetReplicationControllerSummaries();

        Task<IList<ObjectSummary>> GetPersistentVolumeClaimSummaries();

        Task<IList<ObjectSummary>> GetStatefulSetSummaries();
    }
}
