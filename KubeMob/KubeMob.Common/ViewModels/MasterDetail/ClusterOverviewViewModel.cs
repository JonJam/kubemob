using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KubeMob.Common.Exceptions;
using KubeMob.Common.Resx;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.Services.PubSub;
using KubeMob.Common.ViewModels.Base;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Entry = Microcharts.Entry;

namespace KubeMob.Common.ViewModels.MasterDetail
{
    /// <summary>
    /// Due to the limtations of the graph library using, have to specify UI controls and styling in here.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ClusterOverviewViewModel : ViewModelBase
    {
        private readonly IPopupService popupService;
        private readonly IKubernetesService kubernetesService;

        private Chart cronJobs;
        private Chart daemonSets;
        private Chart deployments;
        private Chart jobs;
        private Chart pods;
        private Chart replicaSets;
        private Chart replicationControllers;
        private Chart statefulSets;

        public ClusterOverviewViewModel(
            IKubernetesService kubernetesService,
            IPopupService popupService,
            IPubSubService pubSubService)
        {
            this.kubernetesService = kubernetesService;
            this.popupService = popupService;

            // Defaulting this to true in order that we do not display an empty message on first
            // navigating to this page.
            this.IsBusy = true;

            pubSubService.SubscribeToResourceListingSettingChanged<IKubernetesService>(
                this,
                this.HandleResourceListingSettingChanged);

            pubSubService.SubscribeToNamespaceChanged<IKubernetesService>(
                this,
                this.HandleNamespaceChanged);
        }

        public Chart CronJobs
        {
            get => this.cronJobs;
            private set
            {
                if (this.SetProperty(ref this.cronJobs, value))
                {
                    this.NotifyPropertyChanged(() => this.NoCharts);
                }
            }
        }

        public Chart DaemonSets
        {
            get => this.daemonSets;
            private set
            {
                if (this.SetProperty(ref this.daemonSets, value))
                {
                    this.NotifyPropertyChanged(() => this.NoCharts);
                }
            }
        }

        public Chart Deployments
        {
            get => this.deployments;
            private set
            {
                if (this.SetProperty(ref this.deployments, value))
                {
                    this.NotifyPropertyChanged(() => this.NoCharts);
                }
            }
        }

        public Chart Jobs
        {
            get => this.jobs;
            private set
            {
                if (this.SetProperty(ref this.jobs, value))
                {
                    this.NotifyPropertyChanged(() => this.NoCharts);
                }
            }
        }

        public Chart Pods
        {
            get => this.pods;
            private set
            {
                if (this.SetProperty(ref this.pods, value))
                {
                    this.NotifyPropertyChanged(() => this.NoCharts);
                }
            }
        }

        public Chart ReplicaSets
        {
            get => this.replicaSets;
            private set
            {
                if (this.SetProperty(ref this.replicaSets, value))
                {
                    this.NotifyPropertyChanged(() => this.NoCharts);
                }
            }
        }

        public Chart ReplicationControllers
        {
            get => this.replicationControllers;
            private set
            {
                if (this.SetProperty(ref this.replicationControllers, value))
                {
                    this.NotifyPropertyChanged(() => this.NoCharts);
                }
            }
        }

        public Chart StatefulSets
        {
            get => this.statefulSets;
            private set
            {
                if (this.SetProperty(ref this.statefulSets, value))
                {
                    this.NotifyPropertyChanged(() => this.NoCharts);
                }
            }
        }

        public bool NoCharts => this.CronJobs == null &&
                                this.DaemonSets == null &&
                                this.Deployments == null &&
                                this.Jobs == null &&
                                this.Pods == null &&
                                this.ReplicaSets == null &&
                                this.ReplicationControllers == null &&
                                this.StatefulSets == null;

        public override Task Initialize(object navigationData) => this.PerformNetworkOperation(async () =>
                                                                            {
                                                                                try
                                                                                {
                                                                                    List<Task> tasks = new List<Task>();

                                                                                    if (this.kubernetesService.ShowCronJobs)
                                                                                    {
                                                                                        tasks.Add(this.CreateCronJobsGraph());
                                                                                    }

                                                                                    if (this.kubernetesService.ShowDaemonSets)
                                                                                    {
                                                                                        tasks.Add(this.CreateDaemonSetsGraph());
                                                                                    }

                                                                                    if (this.kubernetesService.ShowDeployments)
                                                                                    {
                                                                                        tasks.Add(this.CreateDeploymentsGraph());
                                                                                    }

                                                                                    if (this.kubernetesService.ShowJobs)
                                                                                    {
                                                                                        tasks.Add(this.CreateJobsGraph());
                                                                                    }

                                                                                    if (this.kubernetesService.ShowPods)
                                                                                    {
                                                                                        tasks.Add(this.CreatePodsGraph());
                                                                                    }

                                                                                    if (this.kubernetesService.ShowReplicaSets)
                                                                                    {
                                                                                        tasks.Add(this.CreateReplicaSetsGraph());
                                                                                    }

                                                                                    if (this.kubernetesService.ShowReplicationControllers)
                                                                                    {
                                                                                        tasks.Add(this.CreateReplicationControllersGraph());
                                                                                    }

                                                                                    if (this.kubernetesService.ShowStatefulSets)
                                                                                    {
                                                                                        tasks.Add(this.CreateStatefulSetsGraph());
                                                                                    }

                                                                                    await Task.WhenAll(tasks);
                                                                                }
                                                                                catch (ClusterNotFoundException)
                                                                                {
                                                                                    await this.popupService.DisplayAlert(
                                                                                        AppResources.ClusterNotFound_Title,
                                                                                        AppResources.ClusterNotFound_Message,
                                                                                        AppResources.OkAlertText);
                                                                                }
                                                                                catch (AccountInvalidException)
                                                                                {
                                                                                    await this.popupService.DisplayAlert(
                                                                                        AppResources.AccountInvalid_Title,
                                                                                        AppResources.AccountInvalid_Message,
                                                                                        AppResources.OkAlertText);
                                                                                }
                                                                            });

        private async Task CreateCronJobsGraph()
        {
            IList<ObjectSummary> cronJobSummaries = await this.kubernetesService.GetCronJobSummaries();

            this.CronJobs = this.CreateChart(cronJobSummaries, AppResources.ClusterOverviewViewModel_Suspended);
        }

        private async Task CreateDaemonSetsGraph()
        {
            IList<ObjectSummary> daemonSetSummaries = await this.kubernetesService.GetDaemonSetSummaries();

            this.DaemonSets = this.CreateChart(daemonSetSummaries);
        }

        private async Task CreateDeploymentsGraph()
        {
            IList<ObjectSummary> deploymentSummaries = await this.kubernetesService.GetDeploymentSummaries();

            this.Deployments = this.CreateChart(deploymentSummaries);
        }

        private async Task CreateJobsGraph()
        {
            IList<ObjectSummary> jobSummaries = await this.kubernetesService.GetJobSummaries(null);

            // Looking at the Kubernetes Dashboard source code, there is a Running state that
            // should be treated different to Succeeded. However this is mismatch with how the status icons work
            // which doesn't handle Running specically; keep consistency and using status icon approach.
            this.Jobs = this.CreateChart(jobSummaries, successText: AppResources.ClusterOverviewViewModel_Succeeded);
        }

        private async Task CreatePodsGraph()
        {
            IList<ObjectSummary> podsSummaries = await this.kubernetesService.GetPodSummaries(null);

            this.Pods = this.CreateChart(podsSummaries);
        }

        private async Task CreateReplicaSetsGraph()
        {
            IList<ObjectSummary> replicaSetSummaries = await this.kubernetesService.GetReplicaSetSummaries(null);

            this.ReplicaSets = this.CreateChart(replicaSetSummaries);
        }

        private async Task CreateReplicationControllersGraph()
        {
            IList<ObjectSummary> replicationControllerSummaries = await this.kubernetesService.GetReplicationControllerSummaries();

            this.ReplicationControllers = this.CreateChart(replicationControllerSummaries);
        }

        private async Task CreateStatefulSetsGraph()
        {
            IList<ObjectSummary> statefulSetsGraph = await this.kubernetesService.GetStatefulSetSummaries();

            this.StatefulSets = this.CreateChart(statefulSetsGraph);
        }

        private Chart CreateChart(
            IList<ObjectSummary> summaries,
            string errorText = null,
            string successText = null)
        {
            if (errorText == null)
            {
                errorText = AppResources.ClusterOverviewViewModel_Failed;
            }

            if (successText == null)
            {
                successText = AppResources.ClusterOverviewViewModel_Running;
            }

            if (!summaries.Any())
            {
                // If there are no summaries, then return null so graph is not displayed.
                return null;
            }

            int success = summaries.Count(c => c.IsStatusSuccess);
            int pending = summaries.Count(c => c.IsStatusPending);
            int error = summaries.Count(c => c.IsStatusError);

            List<Entry> entries = new List<Entry>();

            if (success > 0)
            {
                entries.Add(new Entry(success)
                {
                    Label = successText,
                    ValueLabel = $"{success}",
                    Color = SKColor.Parse((string)Application.Current.Resources["GreenStatusIconColor"])
                });
            }

            if (pending > 0)
            {
                entries.Add(new Entry(pending)
                {
                    Label = AppResources.ClusterOverviewViewModel_Pending,
                    ValueLabel = $"{pending}",
                    Color = SKColor.Parse((string)Application.Current.Resources["GreyStatusIconColor"])
                });
            }

            if (error > 0)
            {
                entries.Add(new Entry(error)
                {
                    Label = errorText,
                    ValueLabel = $"{error}",
                    Color = SKColor.Parse((string)Application.Current.Resources["RedStatusIconColor"])
                });
            }

            return new DonutChart { Entries = entries };
        }

        private async void HandleResourceListingSettingChanged(
            IKubernetesService sender,
            string settingName)
        {
            switch (settingName)
            {
                case nameof(sender.ShowCronJobs):
                case nameof(sender.ShowDaemonSets):
                case nameof(sender.ShowDeployments):
                case nameof(sender.ShowJobs):
                case nameof(sender.ShowPods):
                case nameof(sender.ShowReplicaSets):
                case nameof(sender.ShowReplicationControllers):
                case nameof(sender.ShowStatefulSets):
                    await this.Initialize(null);
                    break;
            }
        }

        private async void HandleNamespaceChanged(
            IKubernetesService sender) => await this.Initialize(null);
    }
}