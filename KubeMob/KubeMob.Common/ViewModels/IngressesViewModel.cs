using System.Collections.Generic;
using System.Threading.Tasks;
using KubeMob.Common.Exceptions;
using KubeMob.Common.Resx;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels
{
    [Preserve(AllMembers = true)]
    public class IngressesViewModel : ViewModelBase
    {
        private readonly IKubernetesService kubernetesService;
        private readonly IPopupService popupService;

        private IList<ObjectSummary> ingresses = new List<ObjectSummary>();
        private bool hasNoNetwork;

        public IngressesViewModel(
            IKubernetesService kubernetesService,
            IPopupService popupService)
        {
            this.kubernetesService = kubernetesService;
            this.popupService = popupService;

            // Defaulting this to true in order that we do not display an empty message on first
            // navigating to this page.
            this.IsBusy = true;
        }

        public IList<ObjectSummary> Ingresses
        {
            get => this.ingresses;
            private set
            {
                if (this.SetProperty(ref this.ingresses, value))
                {
                    this.NotifyPropertyChanged(() => this.HasIngresses);
                }
            }
        }

        public bool HasNoNetwork
        {
            get => this.hasNoNetwork;
            set => this.SetProperty(ref this.hasNoNetwork, value);
        }

        public bool HasIngresses => this.Ingresses.Count > 0;

        public override async Task Initialize(object navigationData)
        {
            // TODO Might want to refactor this to be resuable across pages ??
            this.IsBusy = true;
            this.HasNoNetwork = false;

            try
            {
                this.Ingresses = await this.kubernetesService.GetIngressSummaries();
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
