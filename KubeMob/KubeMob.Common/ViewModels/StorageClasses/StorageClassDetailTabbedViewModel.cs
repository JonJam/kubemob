using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.StorageClasses
{
    [Preserve(AllMembers = true)]
    public class StorageClassDetailTabbedViewModel
        : ObjectDetailTabbedViewModelBase<StorageClassDetailViewModel, StorageClassDetail>
    {
        public StorageClassDetailTabbedViewModel(
            StorageClassDetailViewModel detailVm)
            : base(detailVm)
        {
        }
    }
}
