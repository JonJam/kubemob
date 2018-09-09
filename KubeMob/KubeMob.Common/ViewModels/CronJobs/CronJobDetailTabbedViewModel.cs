using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.CronJobs
{
    [Preserve(AllMembers = true)]
    public class CronJobDetailTabbedViewModel
        : ObjectDetailTabbedViewModelBase<CronJobDetailViewModel, CronJobDetail>
    {
        public CronJobDetailTabbedViewModel(
            CronJobDetailViewModel detailVm)
            : base(detailVm)
        {
        }
    }
}
