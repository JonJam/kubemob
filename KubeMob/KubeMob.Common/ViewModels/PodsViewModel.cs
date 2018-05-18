using System.Collections.Generic;
using System.Threading.Tasks;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Popup;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels
{
    [Preserve(AllMembers = true)]
    public class PodsViewModel : ObjectListViewModelBase
    {
        public PodsViewModel(
            IKubernetesService kubernetesService,
            IPopupService popupService)
            : base(kubernetesService, popupService)
        {
        }

        protected override Task<IList<ObjectSummary>> GetObjectSummaries() => this.KubernetesService.GetPodSummaries();
    }
}
