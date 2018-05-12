using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes
{
    [Preserve(AllMembers = true)]
    public class DeploymentSummary
    {
        public DeploymentSummary(
            string name,
            string replicaRatio)
        {
            this.Name = name;
            this.ReplicaRatio = replicaRatio;
        }

        public string Name
        {
            get;
        }

        public string ReplicaRatio
        {
            get;
        }
    }
}
