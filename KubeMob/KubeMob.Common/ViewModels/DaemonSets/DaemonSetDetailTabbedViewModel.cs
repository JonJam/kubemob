using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.DaemonSets
{
    [Preserve(AllMembers = true)]
    public class DaemonSetDetailTabbedViewModel
        : ObjectDetailTabbedViewModelBase<DaemonSetDetailViewModel, DaemonSetDetail>
    {
        public DaemonSetDetailTabbedViewModel(
            DaemonSetDetailViewModel detailVm)
            : base(detailVm)
        {
        }
    }
}
