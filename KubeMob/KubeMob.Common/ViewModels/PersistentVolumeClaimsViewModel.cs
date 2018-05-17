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
    public class PersistentVolumeClaimsViewModel : ViewModelBase
    {
        private readonly IKubernetesService kubernetesService;
        private readonly IPopupService popupService;

        private IList<PersistentVolumeClaimsSummary> persistentVolumeClaims = new List<PersistentVolumeClaimsSummary>();
        private bool hasNoNetwork;

        public PersistentVolumeClaimsViewModel(
            IKubernetesService kubernetesService,
            IPopupService popupService)
        {
            this.kubernetesService = kubernetesService;
            this.popupService = popupService;

            // Defaulting this to true in order that we do not display an empty message on first
            // navigating to this page.
            this.IsBusy = true;
        }

        public IList<PersistentVolumeClaimsSummary> PersistentVolumeClaims
        {
            get => this.persistentVolumeClaims;
            private set
            {
                if (this.SetProperty(ref this.persistentVolumeClaims, value))
                {
                    this.NotifyPropertyChanged(() => this.HasPersistentVolumeClaims);
                }
            }
        }

        public bool HasNoNetwork
        {
            get => this.hasNoNetwork;
            set => this.SetProperty(ref this.hasNoNetwork, value);
        }

        public bool HasPersistentVolumeClaims => this.PersistentVolumeClaims.Count > 0;

        public override async Task Initialize(object navigationData)
        {
            // TODO Might want to refactor this to be resuable across pages ??
            this.IsBusy = true;
            this.HasNoNetwork = false;

            try
            {
                this.PersistentVolumeClaims = await this.kubernetesService.GetPersistentVolumeClaimSummaries();
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
