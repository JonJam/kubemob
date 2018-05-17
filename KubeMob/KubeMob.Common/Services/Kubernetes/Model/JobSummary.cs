using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class JobSummary
    {
        public JobSummary(
            string name,
            string podRatio)
        {
            this.Name = name;
            this.PodRatio = podRatio;
        }

        public string Name
        {
            get;
        }

        public string PodRatio
        {
            get;
        }
    }
}
