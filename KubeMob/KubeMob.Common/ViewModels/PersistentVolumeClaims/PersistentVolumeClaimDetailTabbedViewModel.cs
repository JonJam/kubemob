using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.PersistentVolumeClaims
{
    [Preserve(AllMembers = true)]
    public class PersistentVolumeClaimDetailTabbedViewModel
        : ObjectDetailTabbedViewModelBase<PersistentVolumeClaimDetailViewModel, PersistentVolumeClaimDetail>
    {
        public PersistentVolumeClaimDetailTabbedViewModel(
            PersistentVolumeClaimDetailViewModel detailVm)
            : base(detailVm)
        {
        }
    }
}
