using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.ReplicaSets
{
    [Preserve(AllMembers = true)]
    public class ReplicaSetDetailTabbedViewModel
        : ObjectDetailTabbedViewModelBase<ReplicaSetDetailViewModel, ReplicaSetDetail>
    {
        public ReplicaSetDetailTabbedViewModel(
            ReplicaSetDetailViewModel detailVm)
            : base(detailVm)
        {
        }
    }
}
