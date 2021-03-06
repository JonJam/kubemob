using System.Collections.Generic;
using System.Threading.Tasks;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes
{
    public interface IKubernetesService
    {
        bool ShowNamespaces
        {
            get;
            set;
        }

        bool ShowNodes
        {
            get;
            set;
        }

        bool ShowPersistentVolumes
        {
            get;
            set;
        }

        bool ShowStorageClasses
        {
            get;
            set;
        }

        bool ShowRoles
        {
            get;
            set;
        }

        bool ShowCronJobs
        {
            get;
            set;
        }

        bool ShowDaemonSets
        {
            get;
            set;
        }

        bool ShowDeployments
        {
            get;
            set;
        }

        bool ShowJobs
        {
            get;
            set;
        }

        bool ShowPods
        {
            get;
            set;
        }

        bool ShowReplicaSets
        {
            get;
            set;
        }

        bool ShowReplicationControllers
        {
            get;
            set;
        }

        bool ShowStatefulSets
        {
            get;
            set;
        }

        bool ShowIngresses
        {
            get;
            set;
        }

        bool ShowServices
        {
            get;
            set;
        }

        bool ShowConfigMaps
        {
            get;
            set;
        }

        bool ShowPersistentVolumeClaims
        {
            get;
            set;
        }

        bool ShowSecrets
        {
            get;
            set;
        }

        void ResetClient();

        Task<IList<Namespace>> GetNamespaces();

        void SetSelectedNamespace(Namespace ns);

        Task<IList<ObjectSummary>> GetNamespaceSummaries();

        Task<NamespaceDetail> GetNamespaceDetail(
            string namespaceName);

        Task<IList<ObjectSummary>> GetNodeSummaries();

        Task<NodeDetail> GetNodeDetail(
            string nodeName);

        Task<IList<ObjectSummary>> GetPersistentVolumeSummaries(
            Filter filter);

        Task<PersistentVolumeDetail> GetPersistentVolumeDetail(
            string persistentVolumeName);

        Task<IList<ObjectSummary>> GetStorageClassesSummaries();

        Task<StorageClassDetail> GetStorageClassDetail(
            string storageClassName);

        Task<IList<ObjectSummary>> GetRolesSummaries();

        Task<IList<ObjectSummary>> GetDeploymentSummaries();

        Task<DeploymentDetail> GetDeploymentDetail(
            string deploymentName,
            string deploymentNamespace);

        Task<IList<ObjectSummary>> GetPodSummaries(Filter filter);

        Task<PodDetail> GetPodDetail(
            string podName,
            string podNamespace);

        Task<IList<ObjectSummary>> GetReplicaSetSummaries(
            Filter filter);

        Task<ReplicaSetDetail> GetReplicaSetDetail(
            string replicaSetName,
            string replicaSetNamespace);

        Task<IList<ObjectSummary>> GetServiceSummaries(
            Filter filter);

        Task<ServiceDetail> GetServiceDetail(
            string serviceName,
            string serviceNamespace);

        Task<IList<ObjectSummary>> GetIngressSummaries();

        Task<IngressDetail> GetIngressDetail(
            string ingressName,
            string ingressNamespace);

        Task<IList<ObjectSummary>> GetConfigMapSummaries();

        Task<ConfigMapDetail> GetConfigMapDetail(
            string configMapName,
            string configMapNamespace);

        Task<IList<ObjectSummary>> GetSecretSummaries();

        Task<SecretDetail> GetSecretDetail(
            string secretName,
            string secretNamespace);

        Task<IList<ObjectSummary>> GetCronJobSummaries();

        Task<CronJobDetail> GetCronJobDetail(
            string cronJobName,
            string cronJobNamespace);

        Task<IList<ObjectSummary>> GetDaemonSetSummaries();

        Task<DaemonSetDetail> GetDaemonSetDetail(
            string daemonSetName,
            string daemonSetNamespace);

        Task<IList<ObjectSummary>> GetJobSummaries(
            Filter filter);

        Task<JobDetail> GetJobDetail(
            string jobName,
            string jobNamespace);

        Task<IList<ObjectSummary>> GetReplicationControllerSummaries();

        Task<ReplicationControllerDetail> GetReplicationControllerDetail(
            string replicationControllerName,
            string replicationControllerNamespace);

        Task<IList<ObjectSummary>> GetPersistentVolumeClaimSummaries(Filter filter);

        Task<PersistentVolumeClaimDetail> GetPersistentVolumeClaimDetail(
            string persistentVolumeClaimName,
            string persistentVolumeClaimNamespace);

        Task<IList<ObjectSummary>> GetStatefulSetSummaries();

        Task<StatefulSetDetail> GetStatefulSetDetail(
            string statefulSetName,
            string statefulSetNamespace);

        Task<IList<ObjectSummary>> GetEventsForObject(Filter filter);

        Task<IList<ObjectSummary>> GetHorizontalPodAutoscalerSummaries(Filter filter);

        Task<HorizontalPodAutoscalerDetail> GetHorizontalPodAutoscalerDetail(
            string horizontalPodAutoscalerName,
            string horizontalPodAutoscalerNamespace);

        Task<IList<ObjectSummary>> GetEndpointSummaries(
            Filter filter);

        Task<EndpointDetail> GetEndpointDetail(
            string endpointName,
            string endpointNamespace);
    }
}
