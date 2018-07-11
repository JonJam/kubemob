using System.Threading.Tasks;
using System.Windows.Input;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.CronJobs
{
    [Preserve(AllMembers = true)]
    public class CronJobDetailViewModel : ObjectDetailViewModelBase<CronJobDetail>
    {
        public CronJobDetailViewModel(
            IKubernetesService kubernetesService,
            IPopupService popupService,
            INavigationService navigationService)
            : base(kubernetesService, popupService, navigationService) => this.ViewRelatedJobsCommand = new Command(async () => await this.OnViewRelatedJobsCommandExecute());

        public ICommand ViewRelatedJobsCommand
        {
            get;
        }

        protected override Task<CronJobDetail> GetObjectDetail(string name, string namespaceName) => this.KubernetesService.GetCronJobDetail(name, namespaceName);

        private Task OnViewRelatedJobsCommandExecute()
        {
            Filter filter = new Filter(this.Detail.NamespaceName, other: this.Detail.Uid);

            return this.NavigationService.NavigateToJobsPage(filter);
        }
    }
}
