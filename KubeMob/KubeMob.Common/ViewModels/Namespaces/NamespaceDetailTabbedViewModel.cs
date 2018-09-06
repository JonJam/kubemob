using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.Namespaces
{
    [Preserve(AllMembers = true)]
    public class NamespaceDetailTabbedViewModel
        : ObjectDetailTabbedViewModelBase<NamespaceDetailViewModel, NamespaceDetail>
    {
        public NamespaceDetailTabbedViewModel(
            NamespaceDetailViewModel detailVm)
            : base(detailVm)
        {
        }
    }
}
