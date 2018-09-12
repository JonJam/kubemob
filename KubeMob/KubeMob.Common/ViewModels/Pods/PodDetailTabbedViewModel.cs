using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.Pods
{
    [Preserve(AllMembers = true)]
    public class PodDetailTabbedViewModel
        : ObjectDetailTabbedViewModelBase<PodDetailViewModel, PodDetail>
    {
        public PodDetailTabbedViewModel(
            PodDetailViewModel detailVm)
            : base(detailVm)
        {
        }
    }
}
