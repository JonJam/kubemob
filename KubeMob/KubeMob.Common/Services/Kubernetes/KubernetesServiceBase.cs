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
using KubeMob.Common.Services.Kubernetes.Extensions;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.PubSub;
using KubeMob.Common.Services.Settings;
using Xamarin.Forms.Internals;
using KubernetesExtensions = KubeMob.Common.Services.Kubernetes.Extensions.KubernetesExtensions;

namespace KubeMob.Common.Services.Kubernetes
{
    // TODO Change IList to IReadonlyList
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

        public bool ShowNamespaces
        {
            get => this.appSettings.ShowNamespaces;
            set
            {
                this.appSettings.ShowNamespaces = value;

                this.pubSubService.PublishResourceListingSettingChanged<IKubernetesService>(
                    this,
                    nameof(this.ShowNamespaces));
            }
        }

        public bool ShowNodes
        {
            get => this.appSettings.ShowNodes;
            set
            {
                this.appSettings.ShowNodes = value;

                this.pubSubService.PublishResourceListingSettingChanged<IKubernetesService>(
                    this,
                    nameof(this.ShowNodes));
            }
        }

        public bool ShowPersistentVolumes
        {
            get => this.appSettings.ShowPersistentVolumes;
            set
            {
                this.appSettings.ShowPersistentVolumes = value;

                this.pubSubService.PublishResourceListingSettingChanged<IKubernetesService>(
                    this,
                    nameof(this.ShowPersistentVolumes));
            }
        }

        public bool ShowStorageClasses
        {
            get => this.appSettings.ShowStorageClasses;
            set
            {
                this.appSettings.ShowStorageClasses = value;

                this.pubSubService.PublishResourceListingSettingChanged<IKubernetesService>(
                    this,
                    nameof(this.ShowStorageClasses));
            }
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

        public async Task<IList<ObjectSummary>> GetNamespaceSummaries()
        {
            V1NamespaceList namespaces = await this.PerformClientOperation((c) => c.ListNamespaceAsync());

            return Mapper.Map<IList<ObjectSummary>>(namespaces.Items)
                .OrderBy(d => d.Name)
                .ToList();
        }

        public async Task<NamespaceDetail> GetNamespaceDetail(
            string namespaceName)
        {
            V1Namespace namespaceDetail = await this.PerformClientOperation((c) => c.ReadNamespaceStatusAsync(namespaceName));

            return Mapper.Map<NamespaceDetail>(namespaceDetail);
        }

        public async Task<IList<ObjectSummary>> GetNodeSummaries()
        {
            V1NodeList nodes = await this.PerformClientOperation((c) => c.ListNodeAsync());

            return Mapper.Map<IList<ObjectSummary>>(nodes.Items)
                .OrderBy(d => d.Name)
                .ToList();
        }

        public async Task<NodeDetail> GetNodeDetail(
            string nodeName)
        {
            V1Node nodeDetail = await this.PerformClientOperation((c) => c.ReadNodeStatusAsync(nodeName));

            return Mapper.Map<NodeDetail>(nodeDetail);
        }

        public async Task<IList<ObjectSummary>> GetPersistentVolumeSummaries(
            Filter filter)
        {
            V1PersistentVolumeList persistentVolumes = await this.PerformClientOperation((c) => c.ListPersistentVolumeAsync());

            IEnumerable<V1PersistentVolume> items = persistentVolumes.Items;

            // Related to Storage Class.
            if (!string.IsNullOrWhiteSpace(filter?.Other))
            {
                items = items.Where(p => p.Spec.StorageClassName == filter.Other);
            }

            return Mapper.Map<IList<ObjectSummary>>(items)
                .OrderBy(d => d.Name)
                .ToList();
        }

        public async Task<PersistentVolumeDetail> GetPersistentVolumeDetail(
            string persistentVolumeName)
        {
            V1PersistentVolume persistentVolumeDetail = await this.PerformClientOperation((c) => c.ReadPersistentVolumeStatusAsync(persistentVolumeName));

            return Mapper.Map<PersistentVolumeDetail>(persistentVolumeDetail);
        }

        public async Task<IList<ObjectSummary>> GetStorageClassesSummaries()
        {
            V1StorageClassList storageClasses = await this.PerformClientOperation((c) => c.ListStorageClassAsync());

            return Mapper.Map<IList<ObjectSummary>>(storageClasses.Items)
                .OrderBy(d => d.Name)
                .ToList();
        }

        public async Task<StorageClassDetail> GetStorageClassDetail(
            string storageClassName)
        {
            V1StorageClass storageClassDetail = await this.PerformClientOperation((c) => c.ReadStorageClassAsync(storageClassName));

            return Mapper.Map<StorageClassDetail>(storageClassDetail);
        }

        public async Task<IList<ObjectSummary>> GetDeploymentSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            // Starting tasks and waiting when all complete.
            Task<V1DeploymentList> deploymentsTask = this.PerformClientOperation((c) => kubernetesNamespace == KubernetesServiceBase.AllNamespace
                 ? c.ListDeploymentForAllNamespacesAsync()
                 : c.ListNamespacedDeploymentAsync(kubernetesNamespace));

            Task<V1PodList> podsTask = this.GetPods(kubernetesNamespace);
            Task<V1EventList> eventsTask = this.GetEvents(kubernetesNamespace);

            await Task.WhenAll(deploymentsTask, podsTask, eventsTask);

            // Tasks already complete here.
            V1DeploymentList deployments = await deploymentsTask;
            V1PodList podList = await podsTask;
            V1EventList events = await eventsTask;

            return Mapper.Map<IList<ObjectSummary>>(
                    deployments.Items,
                    options =>
                    {
                        options.Items[KubernetesExtensions.PodsKey] = podList.Items;
                        options.Items[KubernetesExtensions.EventsKey] = events.Items;
                    })
                .OrderBy(d => d.Name)
                .ToList();
        }

        public async Task<DeploymentDetail> GetDeploymentDetail(
            string deploymentName,
            string deploymentNamespace)
        {
            V1Deployment deployment = await this.PerformClientOperation((c) => c.ReadNamespacedDeploymentStatusAsync(deploymentName, deploymentNamespace));

            return Mapper.Map<DeploymentDetail>(deployment);
        }

        public async Task<IList<ObjectSummary>> GetPodSummaries(
            Filter filter)
        {
            string kubernetesNamespace = !string.IsNullOrWhiteSpace(filter?.Namespace) ? filter.Namespace : this.GetSelectedNamespaceName();
            string fieldSelector = filter?.FieldSelector;
            string labelSelector = filter?.LabelSelector;

            V1PodList podList = await this.PerformClientOperation((c) => kubernetesNamespace == KubernetesServiceBase.AllNamespace
                ? c.ListPodForAllNamespacesAsync(fieldSelector: fieldSelector, labelSelector: labelSelector)
                : c.ListNamespacedPodAsync(kubernetesNamespace, fieldSelector: fieldSelector, labelSelector: labelSelector));

            IEnumerable<V1Pod> items = podList.Items;

            if (!string.IsNullOrWhiteSpace(filter?.Other))
            {
                items = items.FilterPodsForOwner(filter.Other);
            }

            return Mapper.Map<IList<ObjectSummary>>(items)
                .OrderBy(p => p.Name)
                .ToList()
                .AsReadOnly();
        }

        public async Task<PodDetail> GetPodDetail(
            string podName,
            string podNamespace)
        {
            V1Pod pod = await this.PerformClientOperation((c) => c.ReadNamespacedPodStatusAsync(podName, podNamespace));

            return Mapper.Map<PodDetail>(pod);
        }

        public async Task<IList<ObjectSummary>> GetReplicaSetSummaries(
            Filter filter)
        {
            string kubernetesNamespace = !string.IsNullOrWhiteSpace(filter?.Namespace) ? filter.Namespace : this.GetSelectedNamespaceName();

            V1ReplicaSetList replicaSetList = await this.PerformClientOperation((c) => kubernetesNamespace == KubernetesServiceBase.AllNamespace
                ? c.ListReplicaSetForAllNamespacesAsync()
                : c.ListNamespacedReplicaSetAsync(kubernetesNamespace));

            IEnumerable<V1ReplicaSet> items = replicaSetList.Items;

            // TODO Change OwnerReferences to use UUID not name
            // Related to Deployments.
            if (!string.IsNullOrWhiteSpace(filter?.Other))
            {
                items = items.Where(r => r.Metadata.OwnerReferences.Any(o => o.Name == filter.Other) && r.Spec.Replicas.GetValueOrDefault(0) != 0);
            }

            return Mapper.Map<IList<ObjectSummary>>(items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<ReplicaSetDetail> GetReplicaSetDetail(
            string replicaSetName,
            string replicaSetNamespace)
        {
            V1ReplicaSet replicaSetDetail = await this.PerformClientOperation((c) => c.ReadNamespacedReplicaSetStatusAsync(replicaSetName, replicaSetNamespace));

            return Mapper.Map<ReplicaSetDetail>(replicaSetDetail);
        }

        public async Task<IList<ObjectSummary>> GetServiceSummaries(Filter filter)
        {
            string kubernetesNamespace = !string.IsNullOrWhiteSpace(filter?.Namespace) ?
                filter.Namespace :
                this.GetSelectedNamespaceName();

            V1ServiceList serviceList = await this.PerformClientOperation((c) => kubernetesNamespace == KubernetesServiceBase.AllNamespace
                ? c.ListServiceForAllNamespacesAsync()
                : c.ListNamespacedServiceAsync(kubernetesNamespace));

            IList<V1Service> items = serviceList.Items;

            // Related to Daemon Sets, Replica Sets or Replication Controllers.
            if (!string.IsNullOrWhiteSpace(filter?.Other))
            {
                bool IsRelated(V1Service s)
                {
                    IEnumerable<string> labels = s.Spec.Selector?.Select(kvp => $"{kvp.Key}={kvp.Value}");

                    return labels != null && string.Join(",", labels) == filter.Other;
                }

                items = items.Where(IsRelated)
                    .ToList();
            }

            return Mapper.Map<IList<ObjectSummary>>(items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<ServiceDetail> GetServiceDetail(
            string serviceName,
            string serviceNamespace)
        {
            V1Service serviceDetail = await this.PerformClientOperation((c) => c.ReadNamespacedServiceStatusAsync(serviceName, serviceNamespace));

            return Mapper.Map<ServiceDetail>(serviceDetail);
        }

        public async Task<IList<ObjectSummary>> GetIngressSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            V1beta1IngressList ingressList = await this.PerformClientOperation((c) => kubernetesNamespace == KubernetesServiceBase.AllNamespace
                ? c.ListIngressForAllNamespacesAsync()
                : c.ListNamespacedIngressAsync(kubernetesNamespace));

            return Mapper.Map<IList<ObjectSummary>>(ingressList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<IngressDetail> GetIngressDetail(
            string ingressName,
            string ingressNamespace)
        {
            V1beta1Ingress ingressDetail = await this.PerformClientOperation((c) => c.ReadNamespacedIngressStatusAsync(ingressName, ingressNamespace));

            return Mapper.Map<IngressDetail>(ingressDetail);
        }

        public async Task<IList<ObjectSummary>> GetConfigMapSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            V1ConfigMapList configMapList = await this.PerformClientOperation((c) => kubernetesNamespace == KubernetesServiceBase.AllNamespace
                ? c.ListConfigMapForAllNamespacesAsync()
                : c.ListNamespacedConfigMapAsync(kubernetesNamespace));

            return Mapper.Map<IList<ObjectSummary>>(configMapList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<ConfigMapDetail> GetConfigMapDetail(
            string configMapName,
            string configMapNamespace)
        {
            V1ConfigMap configMapDetail = await this.PerformClientOperation((c) => c.ReadNamespacedConfigMapAsync(configMapName, configMapNamespace));

            return Mapper.Map<ConfigMapDetail>(configMapDetail);
        }

        public async Task<IList<ObjectSummary>> GetSecretSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            V1SecretList secretList = await this.PerformClientOperation((c) => kubernetesNamespace == KubernetesServiceBase.AllNamespace
                ? c.ListSecretForAllNamespacesAsync()
                : c.ListNamespacedSecretAsync(kubernetesNamespace));

            return Mapper.Map<IList<ObjectSummary>>(secretList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<SecretDetail> GetSecretDetail(
            string secretName,
            string secretNamespace)
        {
            V1Secret secretDetail = await this.PerformClientOperation((c) => c.ReadNamespacedSecretAsync(secretName, secretNamespace));

            return Mapper.Map<SecretDetail>(secretDetail);
        }

        public async Task<IList<ObjectSummary>> GetCronJobSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            V1beta1CronJobList cronJobList = await this.PerformClientOperation((c) => kubernetesNamespace == KubernetesServiceBase.AllNamespace
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

            return Mapper.Map<CronJobDetail>(cronJobDetail);
        }

        public async Task<IList<ObjectSummary>> GetDaemonSetSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            // Starting tasks and waiting when all complete.
            Task<V1DaemonSetList> daemonSetsTask = this.PerformClientOperation((c) => kubernetesNamespace == KubernetesServiceBase.AllNamespace
                ? c.ListDaemonSetForAllNamespacesAsync()
                : c.ListNamespacedDaemonSetAsync(kubernetesNamespace));

            Task<V1PodList> podsTask = this.GetPods(kubernetesNamespace);
            Task<V1EventList> eventsTask = this.GetEvents(kubernetesNamespace);

            await Task.WhenAll(daemonSetsTask, podsTask, eventsTask);

            // Tasks already complete here.
            V1DaemonSetList daemonSetsList = await daemonSetsTask;
            V1PodList podList = await podsTask;
            V1EventList events = await eventsTask;

            return Mapper.Map<IList<ObjectSummary>>(
                    daemonSetsList.Items,
                    options =>
                    {
                        options.Items[KubernetesExtensions.PodsKey] = podList.Items;
                        options.Items[KubernetesExtensions.EventsKey] = events.Items;
                    })
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<DaemonSetDetail> GetDaemonSetDetail(
            string daemonSetName,
            string daemonSetNamespace)
        {
            V1DaemonSet daemonSetDetail = await this.PerformClientOperation((c) => c.ReadNamespacedDaemonSetStatusAsync(daemonSetName, daemonSetNamespace));

            return Mapper.Map<DaemonSetDetail>(daemonSetDetail);
        }

        public async Task<IList<ObjectSummary>> GetJobSummaries(
            Filter filter)
        {
            string kubernetesNamespace = !string.IsNullOrWhiteSpace(filter?.Namespace) ? filter.Namespace : this.GetSelectedNamespaceName();

            // Starting tasks and waiting when all complete.
            Task<V1JobList> jobsTask = this.PerformClientOperation((c) => kubernetesNamespace == KubernetesServiceBase.AllNamespace
                ? c.ListJobForAllNamespacesAsync()
                : c.ListNamespacedJobAsync(kubernetesNamespace));

            Task<V1PodList> podsTask = this.GetPods(kubernetesNamespace);
            Task<V1EventList> eventsTask = this.GetEvents(kubernetesNamespace);

            await Task.WhenAll(jobsTask, podsTask, eventsTask);

            // Tasks already complete here.
            V1JobList jobList = await jobsTask;
            V1PodList podList = await podsTask;
            V1EventList events = await eventsTask;

            // Related to Cron Jobs.
            IEnumerable<V1Job> items = jobList.Items;

            // TODO Change OwnerReferences to use UUID not name
            if (!string.IsNullOrWhiteSpace(filter?.Other))
            {
                items = items.Where(p => p.Metadata.OwnerReferences.Any(o => o.Name == filter.Other));
            }

            return Mapper.Map<IList<ObjectSummary>>(
                    items,
                    options =>
                    {
                        options.Items[KubernetesExtensions.PodsKey] = podList.Items;
                        options.Items[KubernetesExtensions.EventsKey] = events.Items;
                    })
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<JobDetail> GetJobDetail(
            string jobName,
            string jobNamespace)
        {
            V1Job jobDetail = await this.PerformClientOperation((c) => c.ReadNamespacedJobStatusAsync(jobName, jobNamespace));

            return Mapper.Map<JobDetail>(jobDetail);
        }

        public async Task<IList<ObjectSummary>> GetReplicationControllerSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            V1ReplicationControllerList replicationControllerList = await this.PerformClientOperation((c) => kubernetesNamespace == KubernetesServiceBase.AllNamespace
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

            return Mapper.Map<ReplicationControllerDetail>(replicationControllerDetail);
        }

        public async Task<IList<ObjectSummary>> GetStatefulSetSummaries()
        {
            string kubernetesNamespace = this.GetSelectedNamespaceName();

            V1StatefulSetList statefulSetList = await this.PerformClientOperation((c) => kubernetesNamespace == KubernetesServiceBase.AllNamespace
                ? c.ListStatefulSetForAllNamespacesAsync()
                : c.ListNamespacedStatefulSetAsync(kubernetesNamespace));

            return Mapper.Map<IList<ObjectSummary>>(statefulSetList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<StatefulSetDetail> GetStatefulSetDetail(
            string statefulSetName,
            string statefulSetNamespace)
        {
            V1StatefulSet statefulSetDetail = await this.PerformClientOperation((c) => c.ReadNamespacedStatefulSetStatusAsync(statefulSetName, statefulSetNamespace));

            return Mapper.Map<StatefulSetDetail>(statefulSetDetail);
        }

        public async Task<IList<ObjectSummary>> GetPersistentVolumeClaimSummaries(Filter filter)
        {
            string kubernetesNamespace = !string.IsNullOrWhiteSpace(filter?.Namespace) ? filter.Namespace : this.GetSelectedNamespaceName();

            V1PersistentVolumeClaimList persistentVolumeClaimsList = await this.PerformClientOperation((c) => kubernetesNamespace == KubernetesServiceBase.AllNamespace
                ? c.ListPersistentVolumeClaimForAllNamespacesAsync()
                : c.ListNamespacedPersistentVolumeClaimAsync(kubernetesNamespace));

            IEnumerable<V1PersistentVolumeClaim> items = persistentVolumeClaimsList.Items;

            // Related to Pods.
            if (!string.IsNullOrWhiteSpace(filter?.Other))
            {
                string[] names = filter.Other.Split(',');
                items = items.Where(p => names.Contains(p.Metadata.Name));
            }

            return Mapper.Map<IList<ObjectSummary>>(items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<PersistentVolumeClaimDetail> GetPersistentVolumeClaimDetail(
            string persistentVolumeClaimName,
            string persistentVolumeClaimNamespace)
        {
            V1PersistentVolumeClaim persistentVolumeClaimDetail = await this.PerformClientOperation((c) => c.ReadNamespacedPersistentVolumeClaimAsync(persistentVolumeClaimName, persistentVolumeClaimNamespace));

            return Mapper.Map<PersistentVolumeClaimDetail>(persistentVolumeClaimDetail);
        }

        public async Task<IList<Event>> GetEventsForObject(string objectName, string namespaceName)
        {
            // TODO Change to uuid ?
            V1EventList events = await this.PerformClientOperation((c) => c.ListNamespacedEventAsync(namespaceName, fieldSelector: $"involvedObject.name={objectName}"));

            return Mapper.Map<IList<Event>>(events.Items)
                .OrderBy(e => e.LastSeen)
                .ToList();
        }

        public async Task<IList<ObjectSummary>> GetHorizontalPodAutoscalerSummaries(Filter filter)
        {
            string kubernetesNamespace = !string.IsNullOrWhiteSpace(filter?.Namespace) ? filter.Namespace : this.GetSelectedNamespaceName();

            V1HorizontalPodAutoscalerList horizontalPodAutoscalerList = await this.PerformClientOperation((c) => kubernetesNamespace == KubernetesServiceBase.AllNamespace
                ? c.ListHorizontalPodAutoscalerForAllNamespacesAsync()
                : c.ListNamespacedHorizontalPodAutoscalerAsync(kubernetesNamespace));

            // Related to Deployments.
            IEnumerable<V1HorizontalPodAutoscaler> items = horizontalPodAutoscalerList.Items;

            if (!string.IsNullOrWhiteSpace(filter?.Other))
            {
                items = items.Where(p => p.Spec.ScaleTargetRef.Name == filter.Other);
            }

            return Mapper.Map<IList<ObjectSummary>>(items)
                .OrderBy(p => p.Name)
                .ToList()
                .AsReadOnly();
        }

        public async Task<HorizontalPodAutoscalerDetail> GetHorizontalPodAutoscalerDetail(
            string horizontalPodAutoscalerName,
            string horizontalPodAutoscalerNamespace)
        {
            V1HorizontalPodAutoscaler horizontalPodAutoscaler = await this.PerformClientOperation((c) => c.ReadNamespacedHorizontalPodAutoscalerStatusAsync(horizontalPodAutoscalerName, horizontalPodAutoscalerNamespace));

            return Mapper.Map<HorizontalPodAutoscalerDetail>(horizontalPodAutoscaler);
        }

        public async Task<EndpointDetail> GetEndpointDetail(
            string endpointName,
            string endpointNamespace)
        {
            V1EndpointsList endpointsList = await this.PerformClientOperation((c) => c.ListNamespacedEndpointsAsync(endpointNamespace, fieldSelector: $"metadata.name={endpointName}"));

            return Mapper.Map<EndpointDetail>(endpointsList.Items.FirstOrDefault());
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

        private Task<V1PodList> GetPods(string kubernetesNamespace) =>
            this.PerformClientOperation((c) => kubernetesNamespace == KubernetesServiceBase.AllNamespace
                ? c.ListPodForAllNamespacesAsync()
                : c.ListNamespacedPodAsync(kubernetesNamespace));

        private Task<V1EventList> GetEvents(string kubernetesNamespace) =>
            this.PerformClientOperation((c) => kubernetesNamespace == KubernetesServiceBase.AllNamespace
            ? c.ListEventForAllNamespacesAsync()
            : c.ListNamespacedEventAsync(kubernetesNamespace));
    }
}