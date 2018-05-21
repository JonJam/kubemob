using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using k8s;
using KubeMob.Common.Exceptions;
using KubeMob.Common.Services.AccountManagement;
using KubeMob.Common.Services.AccountManagement.Model;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Settings;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes
{
    public abstract class KubernetesServiceBase : IKubernetesService
    {
        private const string DefaultNamespace = "default";

        private static readonly List<Namespace> InitialNamespaces = new List<Namespace>()
        {
            new Namespace("default", true),
            new Namespace("kube-public"),
            new Namespace("kube-system")
        };

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

        public async Task<IList<Namespace>> GetNamespaces()
        {
            // This is used in the master page. If for any reason we cannot get the list of namespaces, we do not want to
            // display error messages and instead fallback to built in namespaces https://kubernetes.io/docs/concepts/overview/working-with-objects/namespaces/.
            // If this caused by account or cluster issues, whatever detail page that is currently being displayed will handle these cases.
            try
            {
                k8s.Models.V1NamespaceList namespaceList = await this.PerformClientOperation((c) => c.ListNamespaceAsync());

                List<Namespace> namespaces = namespaceList.Items.Select(n => new Namespace(n.Metadata.Name))
                    .OrderBy(d => d.Name)
                    .ToList();

                string selectedNamespaceName = this.GetSelectedNamespaceName();
                Namespace selectedNamespace = namespaces.FirstOrDefault(n => n.Name == selectedNamespaceName);

                if (selectedNamespace == null)
                {
                    // Selected namespace doesn't exist anymomre, so reset.
                    // Using "default" namespace, or if doesn't exist then first in list.
                    selectedNamespace = namespaces.FirstOrDefault(n => n.Name == KubernetesServiceBase.DefaultNamespace) ?? namespaces.First();

                    this.SetSelectedNamespace(selectedNamespace);
                }

                selectedNamespace.IsDefault = true;

                return namespaces;
            }
            catch (NoNetworkException)
            {
                return KubernetesServiceBase.InitialNamespaces;
            }
            catch (ClusterNotFoundException)
            {
                return KubernetesServiceBase.InitialNamespaces;
            }
            catch (AccountInvalidException)
            {
                return KubernetesServiceBase.InitialNamespaces;
            }
        }

        public void SetSelectedNamespace(Namespace ns)
        {
            this.appSettings.SelectedNamespace = ns.Name;

            // TODO Notify of change ?? Will only probably need to on overview.
        }

        public async Task<IList<ObjectSummary>> GetDeploymentSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            k8s.Models.V1DeploymentList deployments = await this.PerformClientOperation((c) => c.ListNamespacedDeploymentAsync(kubernetesNamespace));

            return Mapper.Map<IList<ObjectSummary>>(deployments.Items)
                .OrderBy(d => d.Name)
                .ToList();
        }

        public async Task<IList<ObjectSummary>> GetPodSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            k8s.Models.V1PodList podList = await this.PerformClientOperation((c) => c.ListNamespacedPodAsync(kubernetesNamespace));

            return Mapper.Map<IList<ObjectSummary>>(podList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<IList<ObjectSummary>> GetReplicaSetSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            k8s.Models.V1ReplicaSetList replicaSetList = await this.PerformClientOperation((c) => c.ListNamespacedReplicaSetAsync(kubernetesNamespace));

            return Mapper.Map<IList<ObjectSummary>>(replicaSetList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<IList<ObjectSummary>> GetServiceSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            k8s.Models.V1ServiceList serviceList =
                await this.PerformClientOperation((c) => c.ListNamespacedServiceAsync(kubernetesNamespace));

            return Mapper.Map<IList<ObjectSummary>>(serviceList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<IList<ObjectSummary>> GetIngressSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            k8s.Models.V1beta1IngressList ingressList = await this.PerformClientOperation((c) => c.ListNamespacedIngressAsync(kubernetesNamespace));

            return Mapper.Map<IList<ObjectSummary>>(ingressList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<IList<ObjectSummary>> GetConfigMapSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            k8s.Models.V1ConfigMapList configMapList = await this.PerformClientOperation((c) => c.ListNamespacedConfigMapAsync(kubernetesNamespace));

            return Mapper.Map<IList<ObjectSummary>>(configMapList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<IList<ObjectSummary>> GetSecretSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            k8s.Models.V1SecretList secretList = await this.PerformClientOperation((c) => c.ListNamespacedSecretAsync(kubernetesNamespace));

            return Mapper.Map<IList<ObjectSummary>>(secretList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<IList<ObjectSummary>> GetCronJobSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            k8s.Models.V1beta1CronJobList cronJobList = await this.PerformClientOperation((c) => c.ListNamespacedCronJobAsync(kubernetesNamespace));

            return Mapper.Map<IList<ObjectSummary>>(cronJobList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<IList<ObjectSummary>> GetDaemonSetSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            k8s.Models.V1DaemonSetList daemonSetsList = await this.PerformClientOperation((c) => c.ListNamespacedDaemonSetAsync(kubernetesNamespace));

            return Mapper.Map<IList<ObjectSummary>>(daemonSetsList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<IList<ObjectSummary>> GetJobSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            k8s.Models.V1JobList jobList = await this.PerformClientOperation((c) => c.ListNamespacedJobAsync(kubernetesNamespace));

            return Mapper.Map<IList<ObjectSummary>>(jobList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<IList<ObjectSummary>> GetReplicationControllerSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            k8s.Models.V1ReplicationControllerList replicationControllerList = await this.PerformClientOperation((c) => c.ListNamespacedReplicationControllerAsync(kubernetesNamespace));

            return Mapper.Map<IList<ObjectSummary>>(replicationControllerList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<IList<ObjectSummary>> GetStatefulSetSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            k8s.Models.V1StatefulSetList statefulSetList = await this.PerformClientOperation((c) => c.ListNamespacedStatefulSetAsync(kubernetesNamespace));

            return Mapper.Map<IList<ObjectSummary>>(statefulSetList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<IList<ObjectSummary>> GetPersistentVolumeClaimSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            k8s.Models.V1PersistentVolumeClaimList persistentVolumeClaimsList = await this.PerformClientOperation((c) => c.ListNamespacedPersistentVolumeClaimAsync(kubernetesNamespace));

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

        private string GetSelectedNamespaceName()
        {
            string selectedNamespace = this.appSettings.SelectedNamespace;

            return !string.IsNullOrWhiteSpace(selectedNamespace)
                ? selectedNamespace
                : KubernetesServiceBase.DefaultNamespace;
        }
    }
}
