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
        private bool objectTypeNotSupported;

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

        public bool HasObjectSummaries => this.ObjectSummaries.Count > 0;

        public bool DisplayObjectSummariesInfo => !this.ObjectTypeNotSupported && !this.HasNoNetwork;

        public bool ObjectTypeNotSupported
        {
            get => this.objectTypeNotSupported;
            private set
            {
                if (this.SetProperty(ref this.objectTypeNotSupported, value))
                {
                    this.NotifyPropertyChanged(() => this.DisplayObjectSummariesInfo);
                }
            }
        }

        protected IKubernetesService KubernetesService
        {
            get;
        }

        public override Task Initialize(object navigationData) => this.PerformNetworkOperation(async () =>
        {
            try
            {
                this.ObjectSummaries = await this.GetObjectSummaries();
            }
            catch (AccountInvalidException)
            {
                await this.popupService.DisplayAlert(
                    AppResources.AccountInvalid_Title,
                    AppResources.AccountInvalid_Message,
                    AppResources.OkAlertText);
            }
            catch (ObjectTypeNotSupportedException)
            {
                this.ObjectTypeNotSupported = true;
            }
        });

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(this.HasNoNetwork))
            {
                this.NotifyPropertyChanged(() => this.DisplayObjectSummariesInfo);
            }
        }

        protected abstract Task<IList<ObjectSummary>> GetObjectSummaries();
    }
}
