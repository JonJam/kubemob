using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class CronJobSummary
    {
        public CronJobSummary(
            string name,
            string schedule)
        {
            this.Name = name;
            this.Schedule = schedule;
        }

        public string Name
        {
            get;
        }

        public string Schedule
        {
            get;
        }
    }
}
