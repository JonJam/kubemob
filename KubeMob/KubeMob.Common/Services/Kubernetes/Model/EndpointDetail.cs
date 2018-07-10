using System.Collections.Generic;
using KubeMob.Common.Services.Kubernetes.Model.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class EndpointDetail : ObjectDetailBase
    {
        public EndpointDetail(
            IReadOnlyList<EndpointAddress> addresses)
            : base(string.Empty, string.Empty, string.Empty, new List<string>(), new List<MetadataItem>(), string.Empty) => this.Addresses = addresses;

        public IReadOnlyList<EndpointAddress> Addresses
        {
            get;
        }
    }
}
