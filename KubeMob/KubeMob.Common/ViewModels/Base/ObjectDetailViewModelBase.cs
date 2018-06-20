using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using KubeMob.Common.Exceptions;
using KubeMob.Common.Resx;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Kubernetes.Model.Base;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Services.Popup;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.Base
{
    [Preserve(AllMembers = true)]
    public abstract class ObjectDetailViewModelBase<T> : ViewModelBase
        where T : ObjectDetailBase
    {
        private readonly IPopupService popupService;
        private readonly INavigationService navigationService;

        private string name;
        private T detail;
        private IList<Event> events;

        protected ObjectDetailViewModelBase(
            IKubernetesService kubernetesService,
            IPopupService popupService,
            INavigationService navigationService)
        {
            this.KubernetesService = kubernetesService;
            this.popupService = popupService;
            this.navigationService = navigationService;

            // Defaulting this to true in order that we do not display an empty message on first
            // navigating to this page.
            this.IsBusy = true;

            this.NavigateToEventDetailCommand = new Command(async (o) => await this.OnNavigateToEventDetailCommandExecute(o));
        }

        public ICommand NavigateToEventDetailCommand
        {
            get;
        }

        public string Name
        {
            get => this.name;
            private set => this.SetProperty(ref this.name, value);
        }

        public T Detail
        {
            get => this.detail;
            private set => this.SetProperty(ref this.detail, value);
        }

        public IList<Event> Events
        {
            get => this.events;
            private set => this.SetProperty(ref this.events, value);
        }

        protected IKubernetesService KubernetesService
        {
            get;
        }

        public override async Task Initialize(object navigationData)
        {
            ObjectId objectId = (ObjectId)navigationData;

            this.Name = objectId.Name;

            await this.PerformNetworkOperation(async () =>
            {
                try
                {
                    async Task SetDetail()
                    {
                        this.Detail = await this.GetObjectDetail(objectId.Name, objectId.NamespaceName);
                    }

                    async Task SetEvents()
                    {
                        this.Events =
                            await this.KubernetesService.GetEventsForObject(objectId.Name, objectId.NamespaceName);
                    }

                    async Task SetRelatedObjects()
                    {
                        return this.GetRelatedObjects(objectId.Name, objectId.NamespaceName);
                    }

                    await Task.WhenAll(
                        SetDetail(),
                        SetEvents(),
                        SetRelatedObjects());
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
            });
        }

        protected abstract Task<T> GetObjectDetail(string name, string namespaceName);

        protected virtual Task GetRelatedObjects(string name, string namespaceName) => Task.Completed;

        private async Task OnNavigateToEventDetailCommandExecute(object obj)
        {
            if (obj is Event eventDetail)
            {
                await this.navigationService.NavigateToEventDetailPage(eventDetail);
            }
        }
    }
}
