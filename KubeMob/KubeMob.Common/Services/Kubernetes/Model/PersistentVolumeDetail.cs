using System.Collections.Generic;
using KubeMob.Common.Services.Kubernetes.Model.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class PersistentVolumeDetail : ObjectDetailBase
    {
        public PersistentVolumeDetail(
            string name,
            string namespaceName,
            IReadOnlyList<string> labels,
            IReadOnlyList<string> annotations,
            string creationTime,
            string status,
            string claim,
            string reclaimPolicy,
            IReadOnlyList<string> accessModes,
            string storageClass,
            string reason,
            string message,
            IReadOnlyList<Capacity> capacity)
            : base(name, namespaceName, labels, annotations, creationTime)
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

        // TODO handle link
        public string Claim
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

        // TODO handle link
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