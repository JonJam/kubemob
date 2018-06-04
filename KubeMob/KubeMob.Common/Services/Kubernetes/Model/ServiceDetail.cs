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
            IReadOnlyList<string> labelSelectors,
            string type,
            string sessionAffinity,
            string clusterIp
            )
        {
            this.Name = name;
            this.NamespaceName = namespaceName;
            this.Labels = labels;
            this.Annotations = annotations;
            this.CreationTime = creationTime;
            this.LabelSelectors = labelSelectors;
            this.Type = type;
            this.SessionAffinity = sessionAffinity;
            this.ClusterIp = clusterIp;
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

        public IReadOnlyList<string> LabelSelectors
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
    }
}
