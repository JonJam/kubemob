using System.Collections.Generic;
using System.Threading.Tasks;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.StorageClasses
{
    [Preserve(AllMembers = true)]
    public class StorageClassesViewModel : ObjectListViewModelBase
    {
        public StorageClassesViewModel(
            INavigationService navigationService,
            IKubernetesService kubernetesService,
            IPopupService popupService)
            : base(navigationService, kubernetesService, popupService)
        {
        }

        protected override Task<IList<ObjectSummary>> GetObjectSummaries(Filter filter) => this.KubernetesService.GetStorageClassesSummaries();

        protected override Task OnObjectSummarySelectedExecute(object obj)
        {
            ObjectSummary selected = (ObjectSummary)obj;

            return this.NavigationService.NavigateToStorageClassDetailPage(selected.Name);
        }
    }
}
