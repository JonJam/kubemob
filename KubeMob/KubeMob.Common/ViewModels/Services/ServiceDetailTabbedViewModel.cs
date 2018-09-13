using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.Services
{
    [Preserve(AllMembers = true)]
    public class ServiceDetailTabbedViewModel
        : ObjectDetailTabbedViewModelBase<ServiceDetailViewModel, ServiceDetail>
    {
        public ServiceDetailTabbedViewModel(
            ServiceDetailViewModel detailVm)
            : base(detailVm)
        {
        }
    }
}
