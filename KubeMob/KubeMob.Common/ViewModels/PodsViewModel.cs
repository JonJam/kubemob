using System.Threading.Tasks;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels
{
    [Preserve(AllMembers = true)]
    public class PodsViewModel : ViewModelBase
    {
        private readonly IKubernetesService kubernetesService;

        public PodsViewModel(IKubernetesService kubernetesService)
        {
            this.kubernetesService = kubernetesService;
        }

        public override async Task Initialize(object navigationData)
        {
            await this.kubernetesService.GetPodsSummary();

            base.Initialize(navigationData);
        }
    }
}
