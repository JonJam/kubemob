using System.Threading.Tasks;
using System.Windows.Input;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.DaemonSets
{
    [Preserve(AllMembers = true)]
    public class DaemonSetDetailViewModel : ObjectDetailViewModelBase<DaemonSetDetail>
    {
        public DaemonSetDetailViewModel(
            IKubernetesService kubernetesService,
            IPopupService popupService,
            INavigationService navigationService)
            : base(kubernetesService, popupService, navigationService)
        {
            this.ViewRelatedServicesCommand = new Command(async () => await this.OnViewRelatedServicesCommandExecute());
            this.ViewRelatedPodsCommand = new Command(async () => await this.OnViewRelatedPodsCommandExecute());
        }

        public ICommand ViewRelatedServicesCommand
        {
            get;
        }

        public ICommand ViewRelatedPodsCommand
        {
            get;
        }

        protected override Task<DaemonSetDetail> GetObjectDetail(string name, string namespaceName)
            => this.KubernetesService.GetDaemonSetDetail(name, namespaceName);

        private Task OnViewRelatedServicesCommandExecute()
        {
            Filter filter = new Filter(other: this.Detail.Selector);

            return this.NavigationService.NavigateToServicesPage(filter);
        }

        private Task OnViewRelatedPodsCommandExecute()
        {
            Filter filter = new Filter(other: this.Name);

            return this.NavigationService.NavigateToPodsPage(filter);
        }
    }
}
