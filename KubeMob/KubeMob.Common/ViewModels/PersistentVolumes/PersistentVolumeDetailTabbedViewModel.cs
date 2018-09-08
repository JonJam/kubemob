using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.PersistentVolumes
{
    [Preserve(AllMembers = true)]
    public class PersistentVolumeDetailTabbedViewModel
        : ObjectDetailTabbedViewModelBase<PersistentVolumeDetailViewModel, PersistentVolumeDetail>
    {
        public PersistentVolumeDetailTabbedViewModel(
            PersistentVolumeDetailViewModel detailVm)
            : base(detailVm)
        {
        }
    }
}
