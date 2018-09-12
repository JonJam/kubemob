using System;
using System.Collections.Generic;
using System.Linq;
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
            this.PersistentVolumeClaims = persistentVolumeClaims;

            // I have yet to see a Pod which has more than one owner, to simplify UI just
            // grabbing the first one.
            if (owners.Count > 1)
            {
                throw new ArgumentException("Owners count is greater than 1.");
            }

            this.Owner = owners.First();
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

        public ObjectReference Owner
        {
            get;
        }

        public IReadOnlyList<string> PersistentVolumeClaims
        {
            get;
        }
    }
}
