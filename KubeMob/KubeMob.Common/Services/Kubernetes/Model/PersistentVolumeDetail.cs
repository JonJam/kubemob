using System.Collections.Generic;
using KubeMob.Common.Services.Kubernetes.Model.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class PersistentVolumeDetail : ObjectDetailBase
    {
        public PersistentVolumeDetail(
            string uid,
            string name,
            string namespaceName,
            IReadOnlyList<string> labels,
            IReadOnlyList<MetadataItem> annotations,
            string creationTime,
            string status,
            ObjectReference claim,
            string reclaimPolicy,
            IReadOnlyList<string> accessModes,
            string storageClass,
            string reason,
            string message,
            IReadOnlyList<Capacity> capacity)
            : base(uid, name, namespaceName, labels, annotations, creationTime)
        {
            this.Status = status;
            this.Claim = claim;
            this.ReclaimPolicy = reclaimPolicy;
            this.AccessModes = accessModes;
            this.StorageClass = storageClass;
            this.Reason = reason;
            this.Message = message;
            this.Capacity = capacity;
        }

        public string Status
        {
            get;
        }

        public ObjectReference Claim
        {
            get;
        }

        public string ReclaimPolicy
        {
            get;
        }

        public IReadOnlyList<string> AccessModes
        {
            get;
        }

        public string StorageClass
        {
            get;
        }

        public string Reason
        {
            get;
        }

        public string Message
        {
            get;
        }

        public IReadOnlyList<Capacity> Capacity
        {
            get;
        }
    }
}
