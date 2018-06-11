using System.Threading.Tasks;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.Jobs
{
    [Preserve(AllMembers = true)]
    public class JobDetailViewModel : ObjectDetailViewModelBase<JobDetail>
    {
        public JobDetailViewModel(
            IKubernetesService kubernetesService,
            IPopupService popupService)
            : base(kubernetesService, popupService)
        {
        }

        protected override Task<JobDetail> GetObjectDetail(string name, string namespaceName) => this.KubernetesService.GetJobDetail(name, namespaceName);
    }
}
