using System.Collections.Generic;
using KubeMob.Common.Services.Kubernetes.Model.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class ServiceDetail : ObjectDetailBase
    {
        public ServiceDetail(
            string uid,
            string name,
            string namespaceName,
            IReadOnlyList<string> labels,
            IReadOnlyList<string> annotations,
            string creationTime,
            IReadOnlyList<string> labelSelector,
            string type,
            string sessionAffinity,
            string clusterIp,
            IReadOnlyList<string> internalEndpoints,
            IReadOnlyList<string> externalEndpoints,
            string selector)
            : base(uid, name, namespaceName, labels, annotations, creationTime)
        {
            this.LabelSelector = labelSelector;
            this.Type = type;
            this.SessionAffinity = sessionAffinity;
            this.ClusterIp = clusterIp;
            this.InternalEndpoints = internalEndpoints;
            this.ExternalEndpoints = externalEndpoints;
            this.Selector = selector;
        }

        public IReadOnlyList<string> LabelSelector
        {
            get;
        }

        public string Type
        {
            get;
        }

        public string SessionAffinity
        {
            get;
        }

        public string ClusterIp
        {
            get;
        }

        public IReadOnlyList<string> InternalEndpoints
        {
            get;
        }

        public IReadOnlyList<string> ExternalEndpoints
        {
            get;
        }

        public string Selector
        {
            get;
        }
    }
}
