using System.Threading.Tasks;
using KubeMob.Common.Exceptions;
using KubeMob.Common.Resx;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model.Base;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Services.Popup;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.Base
{
    [Preserve(AllMembers = true)]
    public abstract class ObjectDetailViewModelBase<T> : ViewModelBase
        where T : ObjectDetailBase
    {
        private readonly IPopupService popupService;

        private string name;
        private T detail;

        protected ObjectDetailViewModelBase(
            IKubernetesService kubernetesService,
            IPopupService popupService)
        {
            this.KubernetesService = kubernetesService;
            this.popupService = popupService;

            // Defaulting this to true in order that we do not display an empty message on first
            // navigating to this page.
            this.IsBusy = true;
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
            });
        }

        protected abstract Task<T> GetObjectDetail(string name, string namespaceName);
    }
}
