using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class EndpointAddress
    {
        public EndpointAddress(
            string host,
            string ports,
            string node,
            bool isReady)
        {
            this.Host = host;
            this.Ports = ports;
            this.Node = node;
            this.IsReady = isReady;
        }

        public string Host
        {
            get;
        }

        public string Ports
        {
            get;
        }

        public string Node
        {
            get;
        }

        public bool IsReady
        {
            get;
        }
    }
}
