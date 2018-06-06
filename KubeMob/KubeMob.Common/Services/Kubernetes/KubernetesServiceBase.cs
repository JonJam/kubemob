using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using k8s;
using k8s.Models;
using KubeMob.Common.Exceptions;
using KubeMob.Common.Services.AccountManagement;
using KubeMob.Common.Services.AccountManagement.Model;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.PubSub;
using KubeMob.Common.Services.Settings;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes
{
    [Preserve(AllMembers = true)]
    public abstract class KubernetesServiceBase : IKubernetesService
    {
        private const string AllNamespace = "all";
        private const string DefaultNamespace = "default";

        private static readonly List<Namespace> InitialNamespaces = new List<Namespace>()
        {
            new Namespace(KubernetesServiceBase.AllNamespace),
            new Namespace(KubernetesServiceBase.DefaultNamespace, true),
            new Namespace("kube-public"),
            new Namespace("kube-system")
        };

        private readonly IAppSettings appSettings;
        private readonly IPubSubService pubSubService;
        private readonly IEnumerable<IAccountManager> accountManagers;

        protected KubernetesServiceBase(
            IAppSettings appSettings,
            IPubSubService pubSubService,
            IEnumerable<IAccountManager> accountManagers)
        {
            this.appSettings = appSettings;
            this.pubSubService = pubSubService;
            this.accountManagers = accountManagers;

            this.ResetClient();
        }

        public bool ShowCronJobs
        {
            get => this.appSettings.ShowCronJobs;
            set
            {
                this.appSettings.ShowCronJobs = value;

                this.pubSubService.PublishResourceListingSettingChanged<IKubernetesService>(
                    this,
                    nameof(this.ShowCronJobs));
            }
        }

        public bool ShowDaemonSets
        {
            get => this.appSettings.ShowDaemonSets;
            set
            {
                this.appSettings.ShowDaemonSets = value;

                this.pubSubService.PublishResourceListingSettingChanged<IKubernetesService>(
                    this,
                    nameof(this.ShowDaemonSets));
            }
        }

        public bool ShowDeployments
        {
            get => this.appSettings.ShowDeployments;
            set
            {
                this.appSettings.ShowDeployments = value;

                this.pubSubService.PublishResourceListingSettingChanged<IKubernetesService>(
                    this,
                    nameof(this.ShowDeployments));
            }
        }

        public bool ShowJobs
        {
            get => this.appSettings.ShowJobs;
            set
            {
                this.appSettings.ShowJobs = value;

                this.pubSubService.PublishResourceListingSettingChanged<IKubernetesService>(
                    this,
                    nameof(this.ShowJobs));
            }
        }

        public bool ShowPods
        {
            get => this.appSettings.ShowPods;
            set
            {
                this.appSettings.ShowPods = value;

                this.pubSubService.PublishResourceListingSettingChanged<IKubernetesService>(
                    this,
                    nameof(this.ShowPods));
            }
        }

        public bool ShowReplicaSets
        {
            get => this.appSettings.ShowReplicaSets;
            set
            {
                this.appSettings.ShowReplicaSets = value;

                this.pubSubService.PublishResourceListingSettingChanged<IKubernetesService>(
                    this,
                    nameof(this.ShowReplicaSets));
            }
        }

        public bool ShowReplicationControllers
        {
            get => this.appSettings.ShowReplicationControllers;
            set
            {
                this.appSettings.ShowReplicationControllers = value;

                this.pubSubService.PublishResourceListingSettingChanged<IKubernetesService>(
                    this,
                    nameof(this.ShowReplicationControllers));
            }
        }

        public bool ShowStatefulSets
        {
            get => this.appSettings.ShowStatefulSets;
            set
            {
                this.appSettings.ShowStatefulSets = value;

                this.pubSubService.PublishResourceListingSettingChanged<IKubernetesService>(
                    this,
                    nameof(this.ShowStatefulSets));
            }
        }

        public bool ShowIngresses
        {
            get => this.appSettings.ShowIngresses;
            set
            {
                this.appSettings.ShowIngresses = value;

                this.pubSubService.PublishResourceListingSettingChanged<IKubernetesService>(
                    this,
                    nameof(this.ShowIngresses));
            }
        }

        public bool ShowServices
        {
            get => this.appSettings.ShowServices;
            set
            {
                this.appSettings.ShowServices = value;

                this.pubSubService.PublishResourceListingSettingChanged<IKubernetesService>(
                    this,
                    nameof(this.ShowServices));
            }
        }

        public bool ShowConfigMaps
        {
            get => this.appSettings.ShowConfigMaps;
            set
            {
                this.appSettings.ShowConfigMaps = value;

                this.pubSubService.PublishResourceListingSettingChanged<IKubernetesService>(
                    this,
                    nameof(this.ShowConfigMaps));
            }
        }

        public bool ShowPersistentVolumeClaims
        {
            get => this.appSettings.ShowPersistentVolumeClaims;
            set
            {
                this.appSettings.ShowPersistentVolumeClaims = value;

                this.pubSubService.PublishResourceListingSettingChanged<IKubernetesService>(
                    this,
                    nameof(this.ShowPersistentVolumeClaims));
            }
        }

        public bool ShowSecrets
        {
            get => this.appSettings.ShowSecrets;
            set
            {
                this.appSettings.ShowSecrets = value;

                this.pubSubService.PublishResourceListingSettingChanged<IKubernetesService>(
                    this,
                    nameof(this.ShowSecrets));
            }
        }

        protected Lazy<Task<IKubernetes>> Client
        {
            get;
            private set;
        }

        public void ResetClient() => this.Client = new Lazy<Task<IKubernetes>>(this.CreateClient);

        public async Task<IList<Namespace>> GetNamespaces()
        {
            // This is used in the master page. If for any reason we cannot get the list of namespaces, we do not want to
            // display error messages and instead fallback to built in namespaces https://kubernetes.io/docs/concepts/overview/working-with-objects/namespaces/.
            // If this caused by account or cluster issues, whatever detail page that is currently being displayed will handle these cases.
            try
            {
                k8s.Models.V1NamespaceList namespaceList = await this.PerformClientOperation((c) => c.ListNamespaceAsync());

                List<Namespace> namespaces = namespaceList.Items.Select(n => new Namespace(n.Metadata.Name)).ToList();

                // Add the all namespace.
                namespaces.Add(new Namespace(KubernetesServiceBase.AllNamespace));
                namespaces = namespaces.OrderBy(d => d.Name).ToList();

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

        // TODO Notify of change ?? Will only probably need to on overview.
        public void SetSelectedNamespace(Namespace ns) => this.appSettings.SelectedNamespace = ns.Name;

        public async Task<IList<ObjectSummary>> GetDeploymentSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            k8s.Models.V1DeploymentList deployments = await this.PerformClientOperation((c) => kubernetesNamespace == KubernetesServiceBase.AllNamespace
                ? c.ListDeploymentForAllNamespacesAsync()
                : c.ListNamespacedDeploymentAsync(kubernetesNamespace));

            return Mapper.Map<IList<ObjectSummary>>(deployments.Items)
                .OrderBy(d => d.Name)
                .ToList();
        }

        public async Task<DeploymentDetail> GetDeploymentDetail(
            string deploymentName,
            string deploymentNamespace)
        {
            V1Deployment deployment = await this.PerformClientOperation((c) => c.ReadNamespacedDeploymentStatusAsync(deploymentName, deploymentNamespace));

            // TODO Event information ??
            // TODO New replica set information (info not contained in V1Deployment) ??
            // TODO Old replica set information (info not contained in V1Deployment) ??
            // TODO Horizontal pod autoscaler information (info not contained in V1Deployment) ??
            return Mapper.Map<DeploymentDetail>(deployment);
        }

        public async Task<IList<ObjectSummary>> GetPodSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            k8s.Models.V1PodList podList = await this.PerformClientOperation((c) => kubernetesNamespace == KubernetesServiceBase.AllNamespace
                ? c.ListPodForAllNamespacesAsync()
                : c.ListNamespacedPodAsync(kubernetesNamespace));

            return Mapper.Map<IList<ObjectSummary>>(podList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<PodDetail> GetPodDetail(
            string podName,
            string podNamespace)
        {
            V1Pod pod = await this.PerformClientOperation((c) => c.ReadNamespacedPodStatusAsync(podName, podNamespace));

            // TODO Event information ??
            // Requires another API call ListEventForAllNamespacesAsync and filtering e => e.InvolvedObject.Name == podDetail.Name.
            // Also should only display "non-expired" events (logic needs working out).
            return Mapper.Map<PodDetail>(pod);
        }

        public async Task<IList<ObjectSummary>> GetReplicaSetSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            k8s.Models.V1ReplicaSetList replicaSetList = await this.PerformClientOperation((c) => kubernetesNamespace == KubernetesServiceBase.AllNamespace
                ? c.ListReplicaSetForAllNamespacesAsync()
                : c.ListNamespacedReplicaSetAsync(kubernetesNamespace));

            return Mapper.Map<IList<ObjectSummary>>(replicaSetList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<ReplicaSetDetail> GetReplicaSetDetail(
            string replicaSetName,
            string replicaSetNamespace)
        {
            V1ReplicaSet replicaSetDetail = await this.PerformClientOperation((c) => c.ReadNamespacedReplicaSetStatusAsync(replicaSetName, replicaSetNamespace));

            // TODO Event information ??
            // TODO Pods information (info not contained in V1ReplicaSet)??
            // TODO Services information (info not contained in V1ReplicaSet)??
            // TODO Horizontal pod autoscaler information (info not contained in V1ReplicaSet) ??
            return Mapper.Map<ReplicaSetDetail>(replicaSetDetail);
        }

        public async Task<IList<ObjectSummary>> GetServiceSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            k8s.Models.V1ServiceList serviceList = await this.PerformClientOperation((c) => kubernetesNamespace == KubernetesServiceBase.AllNamespace
                ? c.ListServiceForAllNamespacesAsync()
                : c.ListNamespacedServiceAsync(kubernetesNamespace));

            return Mapper.Map<IList<ObjectSummary>>(serviceList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<ServiceDetail> GetServiceDetail(
            string serviceName,
            string serviceNamespace)
        {
            V1Service serviceDetail = await this.PerformClientOperation((c) => c.ReadNamespacedServiceStatusAsync(serviceName, serviceNamespace));

            // TODO Event information ??
            // TODO Endpoints ??
            // TODO Pods ??
            return Mapper.Map<ServiceDetail>(serviceDetail);
        }

        public async Task<IList<ObjectSummary>> GetIngressSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            k8s.Models.V1beta1IngressList ingressList = await this.PerformClientOperation((c) => kubernetesNamespace == KubernetesServiceBase.AllNamespace
                ? c.ListIngressForAllNamespacesAsync()
                : c.ListNamespacedIngressAsync(kubernetesNamespace));

            return Mapper.Map<IList<ObjectSummary>>(ingressList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<IList<ObjectSummary>> GetConfigMapSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            k8s.Models.V1ConfigMapList configMapList = await this.PerformClientOperation((c) => kubernetesNamespace == KubernetesServiceBase.AllNamespace
                ? c.ListConfigMapForAllNamespacesAsync()
                : c.ListNamespacedConfigMapAsync(kubernetesNamespace));

            return Mapper.Map<IList<ObjectSummary>>(configMapList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<IList<ObjectSummary>> GetSecretSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            k8s.Models.V1SecretList secretList = await this.PerformClientOperation((c) => kubernetesNamespace == KubernetesServiceBase.AllNamespace
                ? c.ListSecretForAllNamespacesAsync()
                : c.ListNamespacedSecretAsync(kubernetesNamespace));

            return Mapper.Map<IList<ObjectSummary>>(secretList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<IList<ObjectSummary>> GetCronJobSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            k8s.Models.V1beta1CronJobList cronJobList = await this.PerformClientOperation((c) => kubernetesNamespace == KubernetesServiceBase.AllNamespace
                ? c.ListCronJobForAllNamespacesAsync()
                : c.ListNamespacedCronJobAsync(kubernetesNamespace));

            return Mapper.Map<IList<ObjectSummary>>(cronJobList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<CronJobDetail> GetCronJobDetail(
            string cronJobName,
            string cronJobNamespace)
        {
            V1beta1CronJob cronJobDetail = await this.PerformClientOperation((c) => c.ReadNamespacedCronJobStatusAsync(cronJobName, cronJobNamespace));

            // TODO Event information ??
            return Mapper.Map<CronJobDetail>(cronJobDetail);
        }

        public async Task<IList<ObjectSummary>> GetDaemonSetSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            k8s.Models.V1DaemonSetList daemonSetsList = await this.PerformClientOperation((c) => kubernetesNamespace == KubernetesServiceBase.AllNamespace
                ? c.ListDaemonSetForAllNamespacesAsync()
                : c.ListNamespacedDaemonSetAsync(kubernetesNamespace));

            return Mapper.Map<IList<ObjectSummary>>(daemonSetsList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<DaemonSetDetail> GetDaemonSetDetail(
            string daemonSetName,
            string daemonSetNamespace)
        {
            V1DaemonSet daemonSetDetail = await this.PerformClientOperation((c) => c.ReadNamespacedDaemonSetStatusAsync(daemonSetName, daemonSetNamespace));

            // TODO Event information ??
            // TODO Services ??
            // TODO Pods ??
            return Mapper.Map<DaemonSetDetail>(daemonSetDetail);
        }

        public async Task<IList<ObjectSummary>> GetJobSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            k8s.Models.V1JobList jobList = await this.PerformClientOperation((c) => kubernetesNamespace == KubernetesServiceBase.AllNamespace
                ? c.ListJobForAllNamespacesAsync()
                : c.ListNamespacedJobAsync(kubernetesNamespace));

            return Mapper.Map<IList<ObjectSummary>>(jobList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<IList<ObjectSummary>> GetReplicationControllerSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            k8s.Models.V1ReplicationControllerList replicationControllerList = await this.PerformClientOperation((c) => kubernetesNamespace == KubernetesServiceBase.AllNamespace
                ? c.ListReplicationControllerForAllNamespacesAsync()
                : c.ListNamespacedReplicationControllerAsync(kubernetesNamespace));

            return Mapper.Map<IList<ObjectSummary>>(replicationControllerList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<ReplicationControllerDetail> GetReplicationControllerDetail(
            string replicationControllerName,
            string replicationControllerNamespace)
        {
            V1ReplicationController replicationControllerDetail = await this.PerformClientOperation((c) => c.ReadNamespacedReplicationControllerStatusAsync(replicationControllerName, replicationControllerNamespace));

            // TODO Event information ??
            // TODO Pods information ??
            // TODO Services information ??
            // TODO Horizontal pod autoscaler information ??
            return Mapper.Map<ReplicationControllerDetail>(replicationControllerDetail);
        }

        public async Task<IList<ObjectSummary>> GetStatefulSetSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            k8s.Models.V1StatefulSetList statefulSetList = await this.PerformClientOperation((c) => kubernetesNamespace == KubernetesServiceBase.AllNamespace
                ? c.ListStatefulSetForAllNamespacesAsync()
                : c.ListNamespacedStatefulSetAsync(kubernetesNamespace));

            return Mapper.Map<IList<ObjectSummary>>(statefulSetList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<IList<ObjectSummary>> GetPersistentVolumeClaimSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            k8s.Models.V1PersistentVolumeClaimList persistentVolumeClaimsList = await this.PerformClientOperation((c) => kubernetesNamespace == KubernetesServiceBase.AllNamespace
                ? c.ListPersistentVolumeClaimForAllNamespacesAsync()
                : c.ListNamespacedPersistentVolumeClaimAsync(kubernetesNamespace));

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
