using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.Secrets
{
    [Preserve(AllMembers = true)]
    public class SecretDetailTabbedViewModel
        : ObjectDetailTabbedViewModelBase<SecretDetailViewModel, SecretDetail>
    {
        public SecretDetailTabbedViewModel(
            SecretDetailViewModel detailVm)
            : base(detailVm)
        {
        }
    }
}
