using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels
{
    [Preserve(AllMembers = true)]
    public class SettingsViewModel : ViewModelBase
    {
        private readonly IKubernetesService kubernetesService;

        public SettingsViewModel(IKubernetesService kubernetesService)
            => this.kubernetesService = kubernetesService;

        public bool ShowCronJobs
        {
            get => this.kubernetesService.ShowCronJobs;
            set
            {
                if (this.kubernetesService.ShowCronJobs != value)
                {
                    this.kubernetesService.ShowCronJobs = value;
                }
            }
        }

        public bool ShowDaemonSets
        {
            get => this.kubernetesService.ShowDaemonSets;
            set
            {
                if (this.kubernetesService.ShowDaemonSets != value)
                {
                    this.kubernetesService.ShowDaemonSets = value;
                }
            }
        }

        public bool ShowDeployments
        {
            get => this.kubernetesService.ShowDeployments;
            set
            {
                if (this.kubernetesService.ShowDeployments != value)
                {
                    this.kubernetesService.ShowDeployments = value;
                }
            }
        }

        public bool ShowJobs
        {
            get => this.kubernetesService.ShowJobs;
            set
            {
                if (this.kubernetesService.ShowJobs != value)
                {
                    this.kubernetesService.ShowJobs = value;
                }
            }
        }

        public bool ShowPods
        {
            get => this.kubernetesService.ShowPods;
            set
            {
                if (this.kubernetesService.ShowPods != value)
                {
                    this.kubernetesService.ShowPods = value;
                }
            }
        }

        public bool ShowReplicaSets
        {
            get => this.kubernetesService.ShowReplicaSets;
            set
            {
                if (this.kubernetesService.ShowReplicaSets != value)
                {
                    this.kubernetesService.ShowReplicaSets = value;
                }
            }
        }

        public bool ShowReplicationControllers
        {
            get => this.kubernetesService.ShowReplicationControllers;
            set
            {
                if (this.kubernetesService.ShowReplicationControllers != value)
                {
                    this.kubernetesService.ShowReplicationControllers = value;
                }
            }
        }

        public bool ShowStatefulSets
        {
            get => this.kubernetesService.ShowStatefulSets;
            set
            {
                if (this.kubernetesService.ShowStatefulSets != value)
                {
                    this.kubernetesService.ShowStatefulSets = value;
                }
            }
        }

        public bool ShowIngresses
        {
            get => this.kubernetesService.ShowIngresses;
            set
            {
                if (this.kubernetesService.ShowIngresses != value)
                {
                    this.kubernetesService.ShowIngresses = value;
                }
            }
        }

        public bool ShowServices
        {
            get => this.kubernetesService.ShowServices;
            set
            {
                if (this.kubernetesService.ShowServices != value)
                {
                    this.kubernetesService.ShowServices = value;
                }
            }
        }

        public bool ShowConfigMaps
        {
            get => this.kubernetesService.ShowConfigMaps;
            set
            {
                if (this.kubernetesService.ShowConfigMaps != value)
                {
                    this.kubernetesService.ShowConfigMaps = value;
                }
            }
        }

        public bool ShowPersistentVolumeClaims
        {
            get => this.kubernetesService.ShowPersistentVolumeClaims;
            set
            {
                if (this.kubernetesService.ShowPersistentVolumeClaims != value)
                {
                    this.kubernetesService.ShowPersistentVolumeClaims = value;
                }
            }
        }

        public bool ShowSecrets
        {
            get => this.kubernetesService.ShowSecrets;
            set
            {
                if (this.kubernetesService.ShowSecrets != value)
                {
                    this.kubernetesService.ShowSecrets = value;
                }
            }
        }
    }
}