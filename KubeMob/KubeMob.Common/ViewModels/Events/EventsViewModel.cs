using System.Collections.Generic;
using System.Threading.Tasks;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.Events
{
    [Preserve(AllMembers = true)]
    public class EventsViewModel : ObjectListViewModelBase
    {
        public EventsViewModel(
            INavigationService navigationService,
            IKubernetesService kubernetesService,
            IPopupService popupService)
            : base(navigationService, kubernetesService, popupService)
        {
        }

        protected override Task<IList<ObjectSummary>> GetObjectSummaries(Filter filter) => this.KubernetesService.GetEventsForObject(filter);

        protected override Task OnObjectSummarySelectedExecute(object obj) => Task.CompletedTask;

        protected override bool OnObjectSummarySelectedCanExecute(object obj) => false;
    }
}