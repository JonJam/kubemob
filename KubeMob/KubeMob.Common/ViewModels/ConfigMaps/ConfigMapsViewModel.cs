using System.Collections.Generic;
using System.Threading.Tasks;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.ConfigMaps
{
    [Preserve(AllMembers = true)]
    public class ConfigMapsViewModel : ObjectListViewModelBase
    {
        public ConfigMapsViewModel(
            INavigationService navigationService,
            IKubernetesService kubernetesService,
            IPopupService popupService)
            : base(navigationService, kubernetesService, popupService)
        {
        }

        protected override Task<IList<ObjectSummary>> GetObjectSummaries() => this.KubernetesService.GetConfigMapSummaries();

        protected override Task OnObjectSummarySelectedExecute(object obj)
        {
            ObjectSummary selected = (ObjectSummary)obj;

            return this.NavigationService.NavigateToConfigMapDetailPage(selected.Name, selected.NamespaceName);
        }
    }
}
