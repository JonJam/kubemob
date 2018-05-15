using System.Collections.Generic;
using System.Threading.Tasks;
using KubeMob.Common.Exceptions;
using KubeMob.Common.Resx;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels
{
    [Preserve(AllMembers = true)]
    public class ReplicationControllersViewModel : ViewModelBase
    {
        private readonly IKubernetesService kubernetesService;
        private readonly IPopupService popupService;

        private IList<ReplicationControllerSummary> replicationControllers = new List<ReplicationControllerSummary>();
        private bool hasNoNetwork;

        public ReplicationControllersViewModel(
            IKubernetesService kubernetesService,
            IPopupService popupService)
        {
            this.kubernetesService = kubernetesService;
            this.popupService = popupService;

            // Defaulting this to true in order that we do not display an empty message on first
            // navigating to this page.
            this.IsBusy = true;
        }

        public IList<ReplicationControllerSummary> ReplicationControllers
        {
            get => this.replicationControllers;
            private set
            {
                if (this.SetProperty(ref this.replicationControllers, value))
                {
                    this.NotifyPropertyChanged(() => this.HasReplicationControllers);
                }
            }
        }

        public bool HasNoNetwork
        {
            get => this.hasNoNetwork;
            set => this.SetProperty(ref this.hasNoNetwork, value);
        }

        public bool HasReplicationControllers => this.ReplicationControllers.Count > 0;

        public override async Task Initialize(object navigationData)
        {
            // TODO Might want to refactor this to be resuable across pages ??
            this.IsBusy = true;
            this.HasNoNetwork = false;

            try
            {
                this.ReplicationControllers = await this.kubernetesService.GetReplicationControllerSummaries();
            }
            catch (NoNetworkException)
            {
                this.HasNoNetwork = true;
            }
            catch (AccountInvalidException)
            {
                await this.popupService.DisplayAlert(
                    AppResources.AccountInvalid_Title,
                    AppResources.AccountInvalid_Message,
                    AppResources.OkAlertText);
            }

            this.IsBusy = false;
        }
    }
}
