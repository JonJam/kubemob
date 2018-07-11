using System.Collections.Generic;
using KubeMob.Common.Services.Kubernetes.Model.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class PodDetail : ObjectDetailBase
    {
        public PodDetail(
            string uid,
            string name,
            string namespaceName,
            IReadOnlyList<MetadataItem> labels,
            IReadOnlyList<MetadataItem> annotations,
            string creationTime,
            string status,
            string qualityOfServiceClass,
            string nodeName,
            string podIpAddress,
            IReadOnlyList<Container> containers,
            IReadOnlyList<Condition> conditions,
            IReadOnlyList<ObjectReference> owners,
            IReadOnlyList<string> persistentVolumeClaims)
            : base(uid, name, namespaceName, labels, annotations, creationTime)
        {
            this.Status = status;
            this.QualityOfServiceClass = qualityOfServiceClass;
            this.NodeName = nodeName;
            this.PodIpAddress = podIpAddress;
            this.Containers = containers;
            this.Conditions = conditions;
            this.Owners = owners;
            this.PersistentVolumeClaims = persistentVolumeClaims;
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

        public IReadOnlyList<Condition> Conditions
        {
            get;
        }

        public IReadOnlyList<ObjectReference> Owners
        {
            get;
        }

        public IReadOnlyList<string> PersistentVolumeClaims
        {
            get;
        }
    }
}
