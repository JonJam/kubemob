using System;
using System.Threading.Tasks;
using System.Windows.Input;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Launcher;
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
        private readonly ILauncher launcher;

        public ServiceDetailViewModel(
            IKubernetesService kubernetesService,
            IPopupService popupService,
            INavigationService navigationService,
            ILauncher launcher)
            : base(kubernetesService, popupService, navigationService)
        {
            this.launcher = launcher;

            this.LaunchExternalEndpointCommand = new Command(this.LaunchExternalEndpointCommandExecute);
        }

        public ICommand LaunchExternalEndpointCommand
        {
            get;
        }

        protected override Task<ServiceDetail> GetObjectDetail(string name, string namespaceName) => this.KubernetesService.GetServiceDetail(name, namespaceName);

        private void LaunchExternalEndpointCommandExecute(object obj)
        {
            string ip = (string)obj;

            string protocol = ip.EndsWith(":443") ? "https" : "http";

            this.launcher.LaunchBrowser(new Uri($"{protocol}://{ip}"));
        }
    }
}
