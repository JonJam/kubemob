using System.Collections.Generic;
using System.Threading.Tasks;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.Pods
{
    [Preserve(AllMembers = true)]
    public class PodsViewModel : ObjectListViewModelBase
    {
        public PodsViewModel(
            INavigationService navigationService,
            IKubernetesService kubernetesService,
            IPopupService popupService)
            : base(navigationService, kubernetesService, popupService)
        {
        }

        protected override Task<IList<ObjectSummary>> GetObjectSummaries(Filter filter)
        {
            // Handling special case where just want to show empty page e.g. for Kybernetes Service's
            // related pods.
            if (filter == null || !filter.IsEmpty)
            {
                return this.KubernetesService.GetPodSummaries(filter);
            }
            else
            {
                return Task.FromResult<IList<ObjectSummary>>(new List<ObjectSummary>());
            }
        }

        protected override Task OnObjectSummarySelectedExecute(object obj)
        {
            ObjectSummary selected = (ObjectSummary)obj;

            return this.NavigationService.NavigateToPodDetailPage(selected.Name, selected.NamespaceName);
        }
    }
}
