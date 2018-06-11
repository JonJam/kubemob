using System.Threading.Tasks;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.DaemonSets
{
    [Preserve(AllMembers = true)]
    public class DaemonSetDetailViewModel : ObjectDetailViewModelBase<DaemonSetDetail>
    {
        public DaemonSetDetailViewModel(
            IKubernetesService kubernetesService,
            IPopupService popupService)
            : base(kubernetesService, popupService)
        {
        }

        protected override Task<DaemonSetDetail> GetObjectDetail(string name, string namespaceName) => this.KubernetesService.GetDaemonSetDetail(name, namespaceName);
    }
}
