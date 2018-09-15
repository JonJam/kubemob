using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.Endpoints
{
    [Preserve(AllMembers = true)]
    public class EndpointDetailTabbedViewModel
        : ObjectDetailTabbedViewModelBase<EndpointDetailViewModel, EndpointDetail>
    {
        public EndpointDetailTabbedViewModel(
            EndpointDetailViewModel detailVm)
            : base(detailVm)
        {
        }
    }
}
