using System.Collections.Generic;
using System.Linq;
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
        private Namespace selectedNamespace;

        public NamespaceSelectorViewModel(IKubernetesService kubernetesService) => this.kubernetesService = kubernetesService;

        public IList<Namespace> Namespaces
        {
            get => this.namespaces;
            private set => this.SetProperty(ref this.namespaces, value);
        }

        public Namespace SelectedNamespace
        {
            get => this.selectedNamespace;
            set
            {
                if (this.SetProperty(ref this.selectedNamespace, value))
                {
                    this.kubernetesService.SetSelectedNamespace(this.selectedNamespace);
                }
            }
        }

        public override async Task Initialize(object navigationData)
        {
            this.Namespaces = await this.kubernetesService.GetNamespaces();

            // Avoiding set this again when initializing.
            this.selectedNamespace = this.Namespaces.First(n => n.IsDefault);
            this.NotifyPropertyChanged(() => this.SelectedNamespace);
        }
    }
}
