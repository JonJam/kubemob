using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class PodDetail
    {
        public PodDetail(
            string name,
            string namespaceName,
            IReadOnlyList<string> labels,
            IReadOnlyList<string> annotations,
            string creationTime,
            string status,
            string qualityOfServiceClass,
            IReadOnlyList<Container> containers,
            IReadOnlyList<PodCondition> conditions,
            IReadOnlyList<OwnerReference> owners)
        {
            this.Name = name;
            this.NamespaceName = namespaceName;
            this.Labels = labels;
            this.Annotations = annotations;
            this.CreationTime = creationTime;
            this.Status = status;
            this.QualityOfServiceClass = qualityOfServiceClass;
            this.Containers = containers;
            this.Conditions = conditions;
            this.Owners = owners;
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

        // TODO Investigate linking to other object type
        public IReadOnlyList<string> Annotations
        {
            get;
        }

        public string CreationTime
        {
            get;
        }

        public string Status
        {
            get;
        }

        public string QualityOfServiceClass
        {
            get;
        }

        // TODO Investigate how environment variables are handled
        public IReadOnlyList<Container> Containers
        {
            get;
        }

        public IReadOnlyList<PodCondition> Conditions
        {
            get;
        }

        // TODO Investigate how handle this link
        public IReadOnlyList<OwnerReference> Owners
        {
            get;
        }
    }
}
