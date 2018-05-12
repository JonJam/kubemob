using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using k8s;
using KubeMob.Common.Services.AccountManagement;
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

        public async Task<IList<DeploymentSummary>> GetDeploymentSummaries()
        {
            // TODO Add filter support
            // TODO Handle API not being supported by cluster
            k8s.Models.V1DeploymentList deployments = await this.GetDeployments();

            return Mapper.Map<IList<DeploymentSummary>>(deployments.Items)
                .OrderBy(d => d.Name)
                .ToList();
        }

        public async Task<IList<PodSummary>> GetPodSummaries()
        {
            // TODO Add filter support - ListNamespacedPodAsync
            // TODO Handle API not being supported by cluster
            k8s.Models.V1PodList podList = await this.GetPods();

            return Mapper.Map<IList<PodSummary>>(podList.Items)
                .OrderBy(p => p.Name)
                .ToList();
        }

        protected abstract Task<k8s.Models.V1DeploymentList> GetDeployments();

        protected abstract Task<k8s.Models.V1PodList> GetPods();

        protected abstract IKubernetes ConfigureClientForPlatform(k8s.Kubernetes client);

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
