using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.Ingresses
{
    [Preserve(AllMembers = true)]
    public class IngressDetailTabbedViewModel
        : ObjectDetailTabbedViewModelBase<IngressDetailViewModel, IngressDetail>
    {
        public IngressDetailTabbedViewModel(
            IngressDetailViewModel detailVm)
            : base(detailVm)
        {
        }
    }
}
