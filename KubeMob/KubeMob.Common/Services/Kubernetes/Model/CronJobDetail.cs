using System.Collections.Generic;
using KubeMob.Common.Services.Kubernetes.Model.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class CronJobDetail : ObjectDetailBase
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
            int activeJobCount,
            string labelSelector)
            : base(name, namespaceName, labels, annotations, creationTime)
        {
            this.Schedule = schedule;
            this.Suspend = suspend;
            this.LastSchedule = lastSchedule;
            this.ConcurrencyPolicy = concurrencyPolicy;
            this.StartingDeadlineSeconds = startingDeadlineSeconds;
            this.ActiveJobCount = activeJobCount;
            this.LabelSelector = labelSelector;
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

        public int ActiveJobCount
        {
            get;
        }

        public string LabelSelector
        {
            get;
        }
    }
}
