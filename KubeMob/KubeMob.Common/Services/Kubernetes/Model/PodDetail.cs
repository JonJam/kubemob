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
            string nodeName,
            string podIpAddress,
            IReadOnlyList<Container> containers,
            IReadOnlyList<PodCondition> conditions,
            IReadOnlyList<OwnerReference> owners,
            IReadOnlyList<string> persistentVolumeClaims)
        {
            this.Name = name;
            this.NamespaceName = namespaceName;
            this.Labels = labels;
            this.Annotations = annotations;
            this.CreationTime = creationTime;
            this.Status = status;
            this.QualityOfServiceClass = qualityOfServiceClass;
            this.NodeName = nodeName;
            this.PodIpAddress = podIpAddress;
            this.Containers = containers;
            this.Conditions = conditions;
            this.Owners = owners;
            this.PersistentVolumeClaims = persistentVolumeClaims;
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

        // TODO Handle links
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

        public string NodeName
        {
            get;
        }

        public string PodIpAddress
        {
            get;
        }

        public IReadOnlyList<Container> Containers
        {
            get;
        }

        public IReadOnlyList<PodCondition> Conditions
        {
            get;
        }

        // TODO Handle links
        public IReadOnlyList<OwnerReference> Owners
        {
            get;
        }

        // TODO Handle links
        public IReadOnlyList<string> PersistentVolumeClaims
        {
            get;
        }
    }
}
