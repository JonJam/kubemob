using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.ConfigMaps
{
    [Preserve(AllMembers = true)]
    public class ConfigMapDetailTabbedViewModel
        : ObjectDetailTabbedViewModelBase<ConfigMapDetailViewModel, ConfigMapDetail>
    {
        public ConfigMapDetailTabbedViewModel(
            ConfigMapDetailViewModel detailVm)
            : base(detailVm)
        {
        }
    }
}
