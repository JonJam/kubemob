using System.Collections.Generic;
using KubeMob.Common.Services.Kubernetes.Model.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class PersistentVolumeClaimDetail : ObjectDetailBase
    {
        public PersistentVolumeClaimDetail(
            string name,
            string namespaceName,
            IReadOnlyList<string> labels,
            IReadOnlyList<string> annotations,
            string creationTime,
            string status,
            string volumeName,
            IReadOnlyList<string> accessModes,
            string storageClass,
            IReadOnlyList<Capacity> capacity)
            : base(name, namespaceName, labels, annotations, creationTime)
        {
            this.Status = status;
            this.VolumeName = volumeName;
            this.AccessModes = accessModes;
            this.StorageClass = storageClass;
            this.Capacity = capacity;
        }

        public string Status
        {
            get;
        }

        // TODO Link
        public string VolumeName
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

        // TODO Link
        public IReadOnlyList<Capacity> Capacity
        {
            get;
        }
    }
}
