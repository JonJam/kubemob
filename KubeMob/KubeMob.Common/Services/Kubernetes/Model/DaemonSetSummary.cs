using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class DaemonSetSummary
    {
        public DaemonSetSummary(
            string name,
            string nodeRatio)
        {
            this.Name = name;
            this.NodeRatio = nodeRatio;
        }

        public string Name
        {
            get;
        }

        public string NodeRatio
        {
            get;
        }
    }
}
