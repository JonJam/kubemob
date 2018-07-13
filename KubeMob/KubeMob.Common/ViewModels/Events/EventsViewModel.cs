using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using KubeMob.Common.Exceptions;
using KubeMob.Common.Resx;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.Events
{
    [Preserve(AllMembers = true)]
    public class EventsViewModel : ViewModelBase
    {
        // TODO see if can re-use ObjectListViewModelBase somehow
        private readonly IPopupService popupService;

        private IList<Event> events = new List<Event>();

        private bool isLoaded = false;

        public EventsViewModel(
            INavigationService navigationService,
            IKubernetesService kubernetesService,
            IPopupService popupService)
        {
            this.NavigationService = navigationService;
            this.KubernetesService = kubernetesService;
            this.popupService = popupService;

            this.EventSelectedCommand = new Command(async (o) => await this.OnEventSelectedExecute(o));

            // Defaulting this to true in order that we do not display an empty message on first
            // navigating to this page.
            this.IsBusy = true;
        }

        public ICommand EventSelectedCommand
        {
            get;
        }

        public IList<Event> Events
        {
            get => this.events;
            private set
            {
                if (this.SetProperty(ref this.events, value))
                {
                    this.NotifyPropertyChanged(() => this.HasEvents);
                }
            }
        }

        public bool HasEvents => this.Events.Count > 0;

        public bool DisplayEvents => !this.HasNoNetwork;

        protected INavigationService NavigationService
        {
            get;
        }

        protected IKubernetesService KubernetesService
        {
            get;
        }

        public override async Task Initialize(object navigationData)
        {
            if (this.isLoaded)
            {
                return;
            }

            await this.PerformNetworkOperation(async () =>
            {
                Filter filter = (Filter)navigationData;

                try
                {
                    this.Events = await this.KubernetesService.GetEventsForObject(filter);
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
                finally
                {
                    this.isLoaded = true;
                }
            });
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(this.HasNoNetwork))
            {
                this.NotifyPropertyChanged(() => this.DisplayEvents);
            }
        }

        private async Task OnEventSelectedExecute(object obj)
        {
            if (obj is Event eventDetail)
            {
                await this.NavigationService.NavigateToEventDetailPage(eventDetail);
            }
        }
    }
}