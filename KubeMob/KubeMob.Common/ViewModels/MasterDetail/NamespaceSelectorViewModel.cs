using KubeMob.Common.Services.Kubernetes;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.MasterDetail
{
    [Preserve(AllMembers = true)]
    public class NamespaceSelectorViewModel : IMenuItem
    {
        private readonly IKubernetesService kubernetesService;

        public NamespaceSelectorViewModel(IKubernetesService kubernetesService)
        {
            this.kubernetesService = kubernetesService;

            // TODO Refactor this fire/forget
            this.Initialize();
        }

        private async void Initialize()
        {
            var a = await this.kubernetesService.GetNamespaces();

            // TODO populate list property
            var b = 1;
        }
    }
}
