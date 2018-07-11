using System.Threading.Tasks;
using System.Windows.Input;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.Jobs
{
    [Preserve(AllMembers = true)]
    public class JobDetailViewModel : ObjectDetailViewModelBase<JobDetail>
    {
        public JobDetailViewModel(
            IKubernetesService kubernetesService,
            IPopupService popupService,
            INavigationService navigationService)
            : base(kubernetesService, popupService, navigationService)
            => this.ViewRelatedPodsCommand = new Command(async () => await this.OnViewRelatedPodsCommandExecute());

        public ICommand ViewRelatedPodsCommand
        {
            get;
        }

        protected override Task<JobDetail> GetObjectDetail(string name, string namespaceName) => this.KubernetesService.GetJobDetail(name, namespaceName);

        private Task OnViewRelatedPodsCommandExecute()
        {
            Filter filter = new Filter(
                this.Detail.NamespaceName,
                other: this.Detail.Uid);

            return this.NavigationService.NavigateToPodsPage(filter);
        }
    }
}
