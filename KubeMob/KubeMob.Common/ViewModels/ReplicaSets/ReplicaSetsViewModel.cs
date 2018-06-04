using System.Collections.Generic;
using System.Threading.Tasks;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.ReplicaSets
{
    [Preserve(AllMembers = true)]
    public class ReplicaSetsViewModel : ObjectListViewModelBase
    {
        public ReplicaSetsViewModel(
            INavigationService navigationService,
            IKubernetesService kubernetesService,
            IPopupService popupService)
            : base(navigationService, kubernetesService, popupService)
        {
        }

        protected override Task<IList<ObjectSummary>> GetObjectSummaries() =>
            this.KubernetesService.GetReplicaSetSummaries();

        protected override Task OnObjectSummarySelectedExecute(object obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
