using System.Threading.Tasks;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.Pods
{
    [Preserve(AllMembers = true)]
    public class PodDetailViewModel : ViewModelBase
    {
        private readonly IKubernetesService kubernetesService;

        public PodDetailViewModel(
            IKubernetesService kubernetesService)
        {
            this.kubernetesService = kubernetesService;
        }

        public override async Task Initialize(object navigationData)
        {
            ObjectId objectId = (ObjectId)navigationData;

            await this.kubernetesService.GetPodDetail(objectId.Name, objectId.NamespaceName);
        }
    }
}
