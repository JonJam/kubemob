using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using KubeMob.Common.Exceptions;
using KubeMob.Common.Resx;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Services.Popup;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.Base
{
    [Preserve(AllMembers = true)]
    public abstract class ObjectListViewModelBase : ViewModelBase
    {
        private readonly IPopupService popupService;

        private IList<ObjectSummary> objectSummaries = new List<ObjectSummary>();
        private bool objectTypeNotSupported;

        protected ObjectListViewModelBase(
            INavigationService navigationService,
            IKubernetesService kubernetesService,
            IPopupService popupService)
        {
            this.NavigationService = navigationService;
            this.KubernetesService = kubernetesService;
            this.popupService = popupService;

            this.ObjectSummarySelectedCommand = new Command(async (o) => await this.OnObjectSummarySelectedExecute(o));

            // Defaulting this to true in order that we do not display an empty message on first
            // navigating to this page.
            this.IsBusy = true;
        }

        public ICommand ObjectSummarySelectedCommand
        {
            get;
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

        protected INavigationService NavigationService
        {
            get;
        }

        protected IKubernetesService KubernetesService
        {
            get;
        }

        public override Task Initialize(object navigationData) => this.PerformNetworkOperation(async () =>
        {
            string fieldSelector = null;

            if (navigationData is string selector &&
                !string.IsNullOrWhiteSpace(selector))
            {
                fieldSelector = selector;
            }

            try
            {
                this.ObjectSummaries = await this.GetObjectSummaries(fieldSelector);
            }
            catch (ClusterNotFoundException)
            {
                await this.popupService.DisplayAlert(
                    AppResources.ClusterNotFound_Title,
                    AppResources.ClusterNotFound_Message,
                    AppResources.OkAlertText);
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

        protected abstract Task<IList<ObjectSummary>> GetObjectSummaries(string fieldSelector);

        protected abstract Task OnObjectSummarySelectedExecute(object obj);
    }
}
