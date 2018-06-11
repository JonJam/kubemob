using System.Threading.Tasks;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.StatefulSets
{
    [Preserve(AllMembers = true)]
    public class StatefulSetDetailViewModel : ObjectDetailViewModelBase<StatefulSetDetail>
    {
        public StatefulSetDetailViewModel(
            IKubernetesService kubernetesService,
            IPopupService popupService)
            : base(kubernetesService, popupService)
        {
        }

        protected override Task<StatefulSetDetail> GetObjectDetail(string name, string namespaceName) => this.KubernetesService.GetStatefulSetDetail(name, namespaceName);
    }
}
