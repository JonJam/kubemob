using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.Deployments
{
    [Preserve(AllMembers = true)]
    public class DeploymentDetailTabbedViewModel
        : ObjectDetailTabbedViewModelBase<DeploymentDetailViewModel, DeploymentDetail>
    {
        public DeploymentDetailTabbedViewModel(
            DeploymentDetailViewModel detailVm)
            : base(detailVm)
        {
        }
    }
}
