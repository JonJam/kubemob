using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class ServiceDetail
    {
        public ServiceDetail(
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
            IReadOnlyList<string> externalEndpoints)
        {
            this.Name = name;
            this.NamespaceName = namespaceName;
            this.Labels = labels;
            this.Annotations = annotations;
            this.CreationTime = creationTime;
            this.LabelSelector = labelSelector;
            this.Type = type;
            this.SessionAffinity = sessionAffinity;
            this.ClusterIp = clusterIp;
            this.InternalEndpoints = internalEndpoints;
            this.ExternalEndpoints = externalEndpoints;
        }

        public string Name
        {
            get;
        }

        public string NamespaceName
        {
            get;
        }

        public IReadOnlyList<string> Labels
        {
            get;
        }

        public IReadOnlyList<string> Annotations
        {
            get;
        }

        public string CreationTime
        {
            get;
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
    }
}
