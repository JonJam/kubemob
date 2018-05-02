using System.Collections.Generic;
using System.Threading.Tasks;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels
{
    [Preserve(AllMembers = true)]
    public class PodsViewModel : ViewModelBase
    {
        private readonly IKubernetesService kubernetesService;

        private IList<PodSummary> pods = new List<PodSummary>();

        public PodsViewModel(IKubernetesService kubernetesService)
        {
            this.kubernetesService = kubernetesService;

            // Defaulting this to true in order that we do not display an empty message on first
            // navigating to this page.
            this.IsBusy = true;
        }

        public IList<PodSummary> Pods
        {
            get => this.pods;
            private set
            {
                if (this.SetProperty(ref this.pods, value))
                {
                    this.NotifyPropertyChanged(() => this.HasPods);
                }
            }
        }

        public bool HasPods => this.Pods.Count > 0;

        public override async Task Initialize(object navigationData)
        {
            this.IsBusy = true;

            // TODO error handling
            this.Pods = await this.kubernetesService.GetPodSummaries();

            this.IsBusy = false;
        }
    }
}
