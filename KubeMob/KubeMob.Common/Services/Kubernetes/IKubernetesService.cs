using System.Collections.Generic;
using System.Threading.Tasks;

namespace KubeMob.Common.Services.Kubernetes
{
    public interface IKubernetesService
    {
        Task<IList<DeploymentSummary>> GetDeploymentSummaries();

        Task<IList<PodSummary>> GetPodSummaries();

        Task<IList<ReplicaSetSummary>> GetReplicaSetSummaries();

        Task<IList<ServiceSummary>> GetServiceSummaries();

        Task<IList<IngressSummary>> GetIngressSummaries();

        Task<IList<ConfigMapSummary>> GetConfigMapSummaries();

        Task<IList<SecretSummary>> GetSecretSummaries();

        Task<IList<CronJobSummary>> GetCronJobSummaries();

        Task<IList<DaemonSetSummary>> GetDaemonSetSummaries();
    }
}
