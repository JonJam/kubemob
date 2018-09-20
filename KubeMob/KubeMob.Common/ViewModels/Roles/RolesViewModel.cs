using System.Collections.Generic;
using System.Threading.Tasks;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.Roles
{
    [Preserve(AllMembers = true)]
    public class RolesViewModel : ObjectListViewModelBase
    {
        public RolesViewModel(
            INavigationService navigationService,
            IKubernetesService kubernetesService,
            IPopupService popupService)
            : base(navigationService, kubernetesService, popupService)
        {
        }

        protected override Task<IList<ObjectSummary>> GetObjectSummaries(Filter filter) => this.KubernetesService.GetRolesSummaries();

        protected override Task OnObjectSummarySelectedExecute(object obj) => throw new System.NotImplementedException();

        protected override bool OnObjectSummarySelectedCanExecute(object obj) => false;
    }
}
