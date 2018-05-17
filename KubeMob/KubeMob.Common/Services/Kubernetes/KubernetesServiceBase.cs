using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using k8s;
using k8s.Models;
using KubeMob.Common.Services.AccountManagement;
using KubeMob.Common.Services.AccountManagement.Model;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Settings;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes
{
    public abstract class KubernetesServiceBase : IKubernetesService
    {
        private readonly IAppSettings appSettings;
        private readonly IEnumerable<IAccountManager> accountManagers;

        [Preserve]
        protected KubernetesServiceBase(
            IAppSettings appSettings,
            IEnumerable<IAccountManager> accountManagers)
        {
            this.appSettings = appSettings;
            this.accountManagers = accountManagers;

            // TODO This will need to be re-created when the user changes their selected cluster.
            this.Client = new Lazy<Task<IKubernetes>>(this.CreateClient);
        }

        protected Lazy<Task<IKubernetes>> Client
        {
            get;
        }

        // TODO Refactor summary methods to use common method

        public async Task<IList<ObjectSummary>> GetDeploymentSummaries()
        {
            // TODO Add filter support
            // TODO Handle API not being supported by cluster
            k8s.Models.V1DeploymentList deployments = await this.PerformClientOperation((c) => c.ListDeploymentForAllNamespacesAsync());

            return Mapper.Map<IList<ObjectSummary>>(deployments.Items)
                .OrderBy(d => d.Name)
                .ToList();
        }

        public async Task<IList<ObjectSummary>> GetPodSummaries()
        {
            // TODO Add filter support - ListNamespacedPodAsync
            // TODO Handle API not being supported by cluster
            k8s.Models.V1PodList podList = await this.PerformClientOperation((c) => c.ListPodForAllNamespacesAsync());

            return Mapper.Map<IList<ObjectSummary>>(podList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<IList<ObjectSummary>> GetReplicaSetSummaries()
        {
            // TODO Add filter support
            // TODO Handle API not being supported by cluster
            k8s.Models.V1ReplicaSetList replicaSetList = await this.PerformClientOperation((c) => c.ListReplicaSetForAllNamespacesAsync());

            return Mapper.Map<IList<ObjectSummary>>(replicaSetList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<IList<ObjectSummary>> GetServiceSummaries()
        {
            // TODO Add filter support
            // TODO Handle API not being supported by cluster
            k8s.Models.V1ServiceList serviceList =
                await this.PerformClientOperation((c) => c.ListServiceForAllNamespacesAsync());

            return Mapper.Map<IList<ObjectSummary>>(serviceList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<IList<ObjectSummary>> GetIngressSummaries()
        {
            // TODO Add filter support
            // TODO Handle API not being supported by cluster
            k8s.Models.V1beta1IngressList ingressList = await this.PerformClientOperation((c) => c.ListIngressForAllNamespacesAsync());

            return Mapper.Map<IList<ObjectSummary>>(ingressList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<IList<ObjectSummary>> GetConfigMapSummaries()
        {
            // TODO Add filter support
            // TODO Handle API not being supported by cluster
            k8s.Models.V1ConfigMapList configMapList = await this.PerformClientOperation((c) => c.ListConfigMapForAllNamespacesAsync());

            return Mapper.Map<IList<ObjectSummary>>(configMapList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<IList<ObjectSummary>> GetSecretSummaries()
        {
            // TODO Add filter support
            // TODO Handle API not being supported by cluster
            k8s.Models.V1SecretList secretList = await this.PerformClientOperation((c) => c.ListSecretForAllNamespacesAsync());

            return Mapper.Map<IList<ObjectSummary>>(secretList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<IList<ObjectSummary>> GetCronJobSummaries()
        {
            // TODO Add filter support
            // TODO Handle API not being supported by cluster
            k8s.Models.V1beta1CronJobList cronJobList = await this.PerformClientOperation((c) => c.ListCronJobForAllNamespacesAsync());

            return Mapper.Map<IList<ObjectSummary>>(cronJobList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<IList<ObjectSummary>> GetDaemonSetSummaries()
        {
            // TODO Add filter support
            // TODO Handle API not being supported by cluster
            k8s.Models.V1DaemonSetList daemonSetsList = await this.PerformClientOperation((c) => c.ListDaemonSetForAllNamespacesAsync());

            return Mapper.Map<IList<ObjectSummary>>(daemonSetsList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<IList<ObjectSummary>> GetJobSummaries()
        {
            // TODO Add filter support
            // TODO Handle API not being supported by cluster
            k8s.Models.V1JobList jobList = await this.PerformClientOperation((c) => c.ListJobForAllNamespacesAsync());

            return Mapper.Map<IList<ObjectSummary>>(jobList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<IList<ObjectSummary>> GetReplicationControllerSummaries()
        {
            // TODO Add filter support
            // TODO Handle API not being supported by cluster
            k8s.Models.V1ReplicationControllerList replicationControllerList = await this.PerformClientOperation((c) => c.ListReplicationControllerForAllNamespacesAsync());

            return Mapper.Map<IList<ObjectSummary>>(replicationControllerList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<IList<ObjectSummary>> GetStatefulSetSummaries()
        {
            // TODO Add filter support
            // TODO Handle API not being supported by cluster
            k8s.Models.V1StatefulSetList statefulSetList = await this.PerformClientOperation((c) => c.ListStatefulSetForAllNamespacesAsync());

            return Mapper.Map<IList<ObjectSummary>>(statefulSetList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<IList<ObjectSummary>> GetPersistentVolumeClaimSummaries()
        {
            // TODO Add filter support
            // TODO Handle API not being supported by cluster
            k8s.Models.V1PersistentVolumeClaimList persistentVolumeClaimsList = await this.PerformClientOperation((c) => c.ListPersistentVolumeClaimForAllNamespacesAsync());

            return Mapper.Map<IList<ObjectSummary>>(persistentVolumeClaimsList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        protected abstract IKubernetes ConfigureClientForPlatform(k8s.Kubernetes client);

        protected abstract Task<T> PerformClientOperation<T>(Func<IKubernetes, Task<T>> clientOperation);

        private async Task<IKubernetes> CreateClient()
        {
            byte[] configContent = await this.GetKubeConfigContent();

            KubernetesClientConfiguration config = null;

            using (Stream stream = new MemoryStream(configContent))
            {
                config = KubernetesClientConfiguration.BuildConfigFromConfigFile(stream);
            }

            k8s.Kubernetes client = new k8s.Kubernetes(config);

            return this.ConfigureClientForPlatform(client);
        }

        private Task<byte[]> GetKubeConfigContent()
        {
            Cluster selectedCluster = this.appSettings.SelectedCluster;

            IAccountManager accountManager = this.accountManagers.First(a => a.Key == selectedCluster.AccountType);

            return accountManager.GetSelectedClusterKubeConfigContent();
        }
    }
}
