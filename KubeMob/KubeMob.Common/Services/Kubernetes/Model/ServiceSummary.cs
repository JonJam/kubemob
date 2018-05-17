using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class ServiceSummary
    {
        public ServiceSummary(
            string name,
            string clusterIp)
        {
            this.Name = name;
            this.ClusterIp = clusterIp;
        }

        public string Name
        {
            get;
        }

        public string ClusterIp
        {
            get;
        }
    }
}
