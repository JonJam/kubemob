using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.HorizontalPodAutoscalers
{
    [Preserve(AllMembers = true)]
    public class HorizontalPodAutoscalerDetailTabbedViewModel
        : ObjectDetailTabbedViewModelBase<HorizontalPodAutoscalerDetailViewModel, HorizontalPodAutoscalerDetail>
    {
        public HorizontalPodAutoscalerDetailTabbedViewModel(
            HorizontalPodAutoscalerDetailViewModel detailVm)
            : base(detailVm)
        {
        }
    }
}
