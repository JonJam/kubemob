using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.Jobs
{
    [Preserve(AllMembers = true)]
    public class JobDetailTabbedViewModel
        : ObjectDetailTabbedViewModelBase<JobDetailViewModel, JobDetail>
    {
        public JobDetailTabbedViewModel(
            JobDetailViewModel detailVm)
            : base(detailVm)
        {
        }
    }
}
