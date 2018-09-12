using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.ReplicationControllers
{
    [Preserve(AllMembers = true)]
    public class ReplicationControllerDetailTabbedViewModel
        : ObjectDetailTabbedViewModelBase<ReplicationControllerDetailViewModel, ReplicationControllerDetail>
    {
        public ReplicationControllerDetailTabbedViewModel(
            ReplicationControllerDetailViewModel detailVm)
            : base(detailVm)
        {
        }
    }
}
