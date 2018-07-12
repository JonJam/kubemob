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

        private T detail;
        private bool objectNotFound;

        protected ObjectDetailViewModelBase(
            IKubernetesService kubernetesService,
            IPopupService popupService,
            INavigationService navigationService)
        {
            this.KubernetesService = kubernetesService;
            this.popupService = popupService;
            this.NavigationService = navigationService;

            // Defaulting this to true in order that we do not display an empty message on first
            // navigating to this page.
            this.IsBusy = true;

            this.DisplayMetadataItemCommand = new Command(async (o) => await this.OnDisplayMetadataItemCommandExecute(o));
        }

        public ICommand DisplayMetadataItemCommand
        {
            get;
        }

        public T Detail
        {
            get => this.detail;
            private set => this.SetProperty(ref this.detail, value);
        }

        public bool DisplayInfo => !this.ObjectNotFound && !this.HasNoNetwork;

        public bool ObjectNotFound
        {
            get => this.objectNotFound;
            private set
            {
                if (this.SetProperty(ref this.objectNotFound, value))
                {
                    this.NotifyPropertyChanged(() => this.DisplayInfo);
                }
            }
        }

        protected IKubernetesService KubernetesService
        {
            get;
        }

        protected INavigationService NavigationService
        {
            get;
        }

        public override async Task Initialize(object navigationData)
        {
            ObjectId objectId = (ObjectId)navigationData;

            await this.PerformNetworkOperation(async () =>
            {
                try
                {
                    // Starting tasks and waiting when all complete.
                    this.Detail = await this.GetObjectDetail(objectId.Name, objectId.NamespaceName);
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
                catch (ObjectNotFoundException)
                {
                    this.ObjectNotFound = true;
                }
            });
        }

        protected abstract Task<T> GetObjectDetail(string name, string namespaceName);
        
        private Task OnDisplayMetadataItemCommandExecute(object obj)
        {
            MetadataItem item = (MetadataItem)obj;

            return this.popupService.DisplayAlert(
                item.Key,
                item.Value,
                AppResources.OkAlertText);
        }
    }
}
