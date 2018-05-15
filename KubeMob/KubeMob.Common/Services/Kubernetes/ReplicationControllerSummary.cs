using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes
{
    [Preserve(AllMembers = true)]
    public class ReplicationControllerSummary
    {
        public ReplicationControllerSummary(
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
