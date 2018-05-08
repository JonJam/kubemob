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

        public async Task<IList<PodSummary>> GetPodSummaries()
        {
            // TODO retry logic
            // TODO Add filter support - ListNamespacedPodAsync
            k8s.Models.V1PodList podList = await this.GetPods();

            return Mapper.Map<IList<PodSummary>>(podList.Items);
        }

        protected abstract Task<k8s.Models.V1PodList> GetPods();

        private async Task<IKubernetes> CreateClient()
        {
            byte[] configContent = await this.GetKubeConfigContent();

            KubernetesClientConfiguration config = null;

            using (Stream stream = new MemoryStream(configContent))
            {
                config = KubernetesClientConfiguration.BuildConfigFromConfigFile(stream);
            }

            return new k8s.Kubernetes(config);
        }

        private Task<byte[]> GetKubeConfigContent()
        {
            Cluster selectedCluster = this.appSettings.SelectedCluster;

            IAccountManager accountManager = this.accountManagers.First(a => a.Key == selectedCluster.AccountType);

            return accountManager.GetSelectedClusterKubeConfigContent();
        }
    }
}
