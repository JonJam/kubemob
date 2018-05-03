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
    public class KubernetesService : IKubernetesService
    {
        private readonly IAppSettings appSettings;
        private readonly IEnumerable<IAccountManager> accountManagers;
        private readonly IKubernetesClientFactory kubernetesClientFactory;

        // TODO Re-arch how get kube context and setup client.
        [Preserve]
        public KubernetesService(
            IAppSettings appSettings,
            IEnumerable<IAccountManager> accountManagers,
            IKubernetesClientFactory kubeFactory)
        {
            this.appSettings = appSettings;
            this.accountManagers = accountManagers;
            this.kubernetesClientFactory = kubeFactory;
        }

        public async Task<IList<PodSummary>> GetPodSummaries()
        {
            byte[] configContent = await this.GetKubeConfigContent();

            KubernetesClientConfiguration config = null;

            using (Stream stream = new MemoryStream(configContent))
            {
                config = KubernetesClientConfiguration.BuildConfigFromConfigFile(stream);
            }

            // TODO Handler errors from this
            IKubernetes client = this.kubernetesClientFactory.CreateClient(config);

            // TODO retry logic
            // TODO exceptions form no internet ?
            // TODO Add filter support - ListNamespacedPodAsync
            k8s.Models.V1PodList podList = await client.ListPodForAllNamespacesAsync();

            return Mapper.Map<IList<PodSummary>>(podList.Items);
        }

        private Task<byte[]> GetKubeConfigContent()
        {
            Cluster selectedCluster = this.appSettings.SelectedCluster;

            IAccountManager accountManager = this.accountManagers.First(a => a.Key == selectedCluster.AccountType);

            return accountManager.GetSelectedClusterKubeConfigContent();
        }
    }
}
