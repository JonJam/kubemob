using System.Collections.Generic;
using System.Threading.Tasks;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.MasterDetail
{
    [Preserve(AllMembers = true)]
    public class NamespaceSelectorViewModel : ViewModelBase, IMenuItem
    {
        private readonly IKubernetesService kubernetesService;

        private IList<Namespace> namespaces;

        public NamespaceSelectorViewModel(IKubernetesService kubernetesService)
        {
            this.kubernetesService = kubernetesService;

            // TODO Refactor this fire/forget
            Task noTask = this.PopulateNamespaces();
        }

        public IList<Namespace> Namespaces
        {
            get => this.namespaces;
            private set => this.SetProperty(ref this.namespaces, value);
        }

        public async Task PopulateNamespaces() => await this.PerformNetworkOperation(async () =>
        {
            this.Namespaces = await this.kubernetesService.GetNamespaces();
        });
    }
}
