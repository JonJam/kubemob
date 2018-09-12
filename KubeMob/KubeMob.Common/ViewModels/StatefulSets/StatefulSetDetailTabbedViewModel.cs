using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.StatefulSets
{
    [Preserve(AllMembers = true)]
    public class StatefulSetDetailTabbedViewModel
        : ObjectDetailTabbedViewModelBase<StatefulSetDetailViewModel, StatefulSetDetail>
    {
        public StatefulSetDetailTabbedViewModel(
            StatefulSetDetailViewModel detailVm)
            : base(detailVm)
        {
        }
    }
}
