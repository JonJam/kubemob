using System.Threading.Tasks;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.Services
{
    [Preserve(AllMembers = true)]
    public class ServiceDetailViewModel : ObjectDetailViewModelBase<ServiceDetail>
    {
        public ServiceDetailViewModel(
            IKubernetesService kubernetesService,
            IPopupService popupService)
            : base(kubernetesService, popupService)
        {
        }

        protected override Task<ServiceDetail> GetObjectDetail(string name, string namespaceName) => this.KubernetesService.GetServiceDetail(name, namespaceName);
    }
}
