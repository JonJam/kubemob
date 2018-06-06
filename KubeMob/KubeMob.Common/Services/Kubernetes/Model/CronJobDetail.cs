using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class CronJobDetail
    {
        public CronJobDetail(
            string name,
            string namespaceName,
            IReadOnlyList<string> labels,
            IReadOnlyList<string> annotations,
            string creationTime,
            string schedule,
            bool suspend,
            string lastSchedule,
            string concurrencyPolicy,
            string startingDeadlineSeconds,
            IReadOnlyList<OwnerReference> activeJobs)
        {
            this.Name = name;
            this.NamespaceName = namespaceName;
            this.Labels = labels;
            this.Annotations = annotations;
            this.CreationTime = creationTime;
            this.Schedule = schedule;
            this.Suspend = suspend;
            this.LastSchedule = lastSchedule;
            this.ConcurrencyPolicy = concurrencyPolicy;
            this.StartingDeadlineSeconds = startingDeadlineSeconds;
            this.ActiveJobs = activeJobs;
        }

        public string Name
        {
            get;
        }

        public string NamespaceName
        {
            get;
        }

        public IReadOnlyList<string> Labels
        {
            get;
        }

        public IReadOnlyList<string> Annotations
        {
            get;
        }

        public string CreationTime
        {
            get;
        }

        public string Schedule
        {
            get;
        }

        public bool Suspend
        {
            get;
        }

        public string LastSchedule
        {
            get;
        }

        public string ConcurrencyPolicy
        {
            get;
        }

        public string StartingDeadlineSeconds
        {
            get;
        }

        // TODO Handle links
        public IReadOnlyList<OwnerReference> ActiveJobs
        {
            get;
        }

        public int ActiveJobCount => this.ActiveJobs.Count;
    }
}
