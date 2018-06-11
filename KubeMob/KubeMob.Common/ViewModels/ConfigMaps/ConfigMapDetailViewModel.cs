using System.Threading.Tasks;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.ConfigMaps
{
    [Preserve(AllMembers = true)]
    public class ConfigMapDetailViewModel : ObjectDetailViewModelBase<ConfigMapDetail>
    {
        public ConfigMapDetailViewModel(
            IKubernetesService kubernetesService,
            IPopupService popupService)
            : base(kubernetesService, popupService)
        {
        }

        protected override Task<ConfigMapDetail> GetObjectDetail(string name, string namespaceName) => this.KubernetesService.GetConfigMapDetail(name, namespaceName);
    }
}
