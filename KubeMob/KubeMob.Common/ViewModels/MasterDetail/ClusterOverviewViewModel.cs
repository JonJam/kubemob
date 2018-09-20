using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KubeMob.Common.Exceptions;
using KubeMob.Common.Resx;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.Services.Popup;
using KubeMob.Common.ViewModels.Base;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms.Internals;
using Entry = Microcharts.Entry;

namespace KubeMob.Common.ViewModels.MasterDetail
{
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
            IPopupService popupService)
        {
            this.kubernetesService = kubernetesService;
            this.popupService = popupService;

            // Defaulting this to true in order that we do not display an empty message on first
            // navigating to this page.
            this.IsBusy = true;
        }

        public Chart CronJobs
        {
            get => this.cronJobs;
            private set => this.SetProperty(ref this.cronJobs, value);
        }

        public Chart DaemonSets
        {
            get => this.daemonSets;
            private set => this.SetProperty(ref this.daemonSets, value);
        }

        public Chart Deployments
        {
            get => this.deployments;
            private set => this.SetProperty(ref this.deployments, value);
        }

        public Chart Jobs
        {
            get => this.jobs;
            private set => this.SetProperty(ref this.jobs, value);
        }

        public Chart Pods
        {
            get => this.pods;
            private set => this.SetProperty(ref this.pods, value);
        }

        public Chart ReplicaSets
        {
            get => this.replicaSets;
            private set => this.SetProperty(ref this.replicaSets, value);
        }

        public Chart ReplicationControllers
        {
            get => this.replicationControllers;
            private set => this.SetProperty(ref this.replicationControllers, value);
        }

        public Chart StatefulSets
        {
            get => this.statefulSets;
            private set => this.SetProperty(ref this.statefulSets, value);
        }

        // TODO Shpw/hide -> empty or toggle

        // TODO Handle namespace change or toggle in settings

        public override Task Initialize(object navigationData)
        {
            return this.PerformNetworkOperation(async () =>
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
        }

        private async Task CreateCronJobsGraph()
        {
            IList<ObjectSummary> cronJobSummaries = await this.kubernetesService.GetCronJobSummaries();

            this.CronJobs = this.CreateChart(cronJobSummaries, "Suspended");
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
            IList<ObjectSummary> jobs = await this.kubernetesService.GetJobSummaries(null);

            // Looking at the Kubernetes Dashboard source code, there is a Running state that
            // should be treated different to Succeeded. However this is mismatch with how the status icons work
            // which doesn't handle Running specically; keep consistency and using status icon approach.
            this.Jobs = this.CreateChart(jobs, successText: "Succeeded");
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
            string errorText = "Failed",
            string successText = "Running")
        {
            int success = summaries.Count(c => c.IsStatusSuccess);
            int pending = summaries.Count(c => c.IsStatusPending);
            int error = summaries.Count(c => c.IsStatusError);

            // TODO Sort out text and colours.
            List<Entry> entries = new List<Entry>();

            if (success > 0)
            {
                entries.Add(new Entry(success)
                {
                    Label = successText,
                    ValueLabel = $"{success}",
                    Color = SKColor.Parse("#008000")
                });
            }

            if (pending > 0)
            {
                entries.Add(new Entry(pending)
                {
                    Label = "Pending",
                    ValueLabel = $"{pending}",
                    Color = SKColor.Parse("#757575")
                });
            }

            if (error > 0)
            {
                entries.Add(new Entry(error)
                {
                    Label = errorText,
                    ValueLabel = $"{error}",
                    Color = SKColor.Parse("#e51c23")
                });
            }

            return new DonutChart() { Entries = entries };
        }
    }
}
