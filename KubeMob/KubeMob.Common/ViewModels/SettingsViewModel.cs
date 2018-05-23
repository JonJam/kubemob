using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels
{
    [Preserve(AllMembers = true)]
    public class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel(IKubernetesService kubernetesService)
            => this.KubernetesService = kubernetesService;

        public IKubernetesService KubernetesService
        {
            get;
        }
    }
}