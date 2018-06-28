using System.Threading.Tasks;
using System.Windows.Input;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.Services
{
    [Preserve(AllMembers = true)]
    public class ServiceDetailViewModel : ObjectDetailViewModelBase<ServiceDetail>
    {
        public ServiceDetailViewModel(
            IKubernetesService kubernetesService,
            IPopupService popupService,
            INavigationService navigationService)
            : base(kubernetesService, popupService, navigationService)
        {
            this.ViewRelatedEndpointsCommand = new Command(async () => await this.OnViewRelatedEndpointsCommandExecute());
            this.ViewRelatedPodsCommand = new Command(async () => await this.OnViewRelatedPodsCommandExecute());
        }

        public ICommand ViewRelatedEndpointsCommand
        {
            get;
        }

        public ICommand ViewRelatedPodsCommand
        {
            get;
        }

        protected override Task<ServiceDetail> GetObjectDetail(string name, string namespaceName) => this.KubernetesService.GetServiceDetail(name, namespaceName);

        private Task OnViewRelatedEndpointsCommandExecute() => this.NavigationService.NavigateToEndpointDetailPage(this.Name, this.NamespaceName);

        private Task OnViewRelatedPodsCommandExecute()
        {
            Filter filter = new Filter(
                this.NamespaceName,
                labelSelector: this.Detail.Selector);

            return this.NavigationService.NavigateToPodsPage(filter);
        }
    }
}
