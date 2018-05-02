using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        
        public async Task GetPodsSummary()
        {
            // TODO Handle errors from this.
            byte[] configContent = await this.GetKubeConfigContent();

            KubernetesClientConfiguration config = null;

            using (Stream stream = new MemoryStream(configContent))
            {
                config = KubernetesClientConfiguration.BuildConfigFromConfigFile(stream);
            }

            IKubernetes client = this.kubernetesClientFactory.CreateClient(config);

            // TODO Add filter support - ListNamespacedPodAsync
            var list = await client.ListPodForAllNamespacesAsync();
            foreach (var item in list.Items)
            {
                string name = item.Metadata.Name;
                string phase = item.Status.Phase;
                string nodeName = item.Spec.NodeName;
            }
        }

        private Task<byte[]> GetKubeConfigContent()
        {
            var selectedCluster = this.appSettings.SelectedCluster;
            
            var accountManager = this.accountManagers.First(a => a.Key == selectedCluster.AccountType);

            return accountManager.GetSelectedClusterKubeConfigContent();
        }
    }
}
