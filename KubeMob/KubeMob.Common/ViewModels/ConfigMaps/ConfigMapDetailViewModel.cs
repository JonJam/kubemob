using System.Threading.Tasks;
using KubeMob.Common.Exceptions;
using KubeMob.Common.Resx;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

// TODO Reduce duplication amongst detail pages
namespace KubeMob.Common.ViewModels.ConfigMaps
{
    [Preserve(AllMembers = true)]
    public class ConfigMapDetailViewModel : ViewModelBase
    {
        private readonly IKubernetesService kubernetesService;
        private readonly IPopupService popupService;

        private string name;
        private ConfigMapDetail detail;

        public ConfigMapDetailViewModel(
            IKubernetesService kubernetesService,
            IPopupService popupService)
        {
            this.kubernetesService = kubernetesService;
            this.popupService = popupService;

            // Defaulting this to true in order that we do not display anything whilst
            // navigating to this page.
            this.IsBusy = true;
        }

        public string Name
        {
            get => this.name;
            private set => this.SetProperty(ref this.name, value);
        }

        public ConfigMapDetail Detail
        {
            get => this.detail;
            private set => this.SetProperty(ref this.detail, value);
        }

        public override async Task Initialize(object navigationData)
        {
            ObjectId objectId = (ObjectId)navigationData;

            this.Name = objectId.Name;

            await this.PerformNetworkOperation(async () =>
            {
                try
                {
                    this.Detail = await this.kubernetesService.GetConfigMapDetail(objectId.Name, objectId.NamespaceName);
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
    }
}
