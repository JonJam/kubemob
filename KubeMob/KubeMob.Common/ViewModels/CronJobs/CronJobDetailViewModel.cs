using System.Threading.Tasks;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.CronJobs
{
    [Preserve(AllMembers = true)]
    public class CronJobDetailViewModel : ObjectDetailViewModelBase<CronJobDetail>
    {
        public CronJobDetailViewModel(
            IKubernetesService kubernetesService,
            IPopupService popupService)
            : base(kubernetesService, popupService)
        {
        }

        protected override Task<CronJobDetail> GetObjectDetail(string name, string namespaceName) => this.KubernetesService.GetCronJobDetail(name, namespaceName);
    }
}
