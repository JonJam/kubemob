using System.Collections.Generic;
using KubeMob.Common.Services.Kubernetes.Model.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class PersistentVolumeClaimDetail : ObjectDetailBase
    {
        public PersistentVolumeClaimDetail(
            string uid,
            string name,
            string namespaceName,
            IReadOnlyList<MetadataItem> labels,
            IReadOnlyList<MetadataItem> annotations,
            string creationTime,
            string status,
            string volumeName,
            string accessModes,
            string storageClass,
            IReadOnlyList<Capacity> capacity)
            : base(uid, name, namespaceName, labels, annotations, creationTime)
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

        public string VolumeName
        {
            get;
        }

        public string AccessModes
        {
            get;
        }

        public string StorageClass
        {
            get;
        }

        public IReadOnlyList<Capacity> Capacity
        {
            get;
        }
    }
}
