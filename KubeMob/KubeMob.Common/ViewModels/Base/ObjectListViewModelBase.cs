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
    public abstract class ObjectListViewModelBase : ViewModelBase
    {
        private readonly IPopupService popupService;

        private IList<ObjectSummary> objectSummaries = new List<ObjectSummary>();
        private bool hasNoNetwork;

        protected ObjectListViewModelBase(
            IKubernetesService kubernetesService,
            IPopupService popupService)
        {
            this.KubernetesService = kubernetesService;
            this.popupService = popupService;

            // Defaulting this to true in order that we do not display an empty message on first
            // navigating to this page.
            this.IsBusy = true;
        }

        public IList<ObjectSummary> ObjectSummaries
        {
            get => this.objectSummaries;
            private set
            {
                if (this.SetProperty(ref this.objectSummaries, value))
                {
                    this.NotifyPropertyChanged(() => this.HasObjectSummaries);
                }
            }
        }

        public bool HasNoNetwork
        {
            get => this.hasNoNetwork;
            set => this.SetProperty(ref this.hasNoNetwork, value);
        }

        public bool HasObjectSummaries => this.ObjectSummaries.Count > 0;

        protected IKubernetesService KubernetesService
        {
            get;
        }

        public override async Task Initialize(object navigationData)
        {
            // TODO HasNoNetwork out to be reusable??
            this.IsBusy = true;
            this.HasNoNetwork = false;

            try
            {
                this.ObjectSummaries = await this.GetObjectSummaries();
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

        protected abstract Task<IList<ObjectSummary>> GetObjectSummaries();
    }
}
