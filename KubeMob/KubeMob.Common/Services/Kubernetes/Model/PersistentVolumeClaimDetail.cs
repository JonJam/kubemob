using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class PersistentVolumeClaimDetail
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
            string storageClassName,
            List<Capacity> capacity)
        {
            this.Name = name;
            this.NamespaceName = namespaceName;
            this.Labels = labels;
            this.Annotations = annotations;
            this.CreationTime = creationTime;
            this.Status = status;
            this.VolumeName = volumeName;
            this.AccessModes = accessModes;
            this.StorageClassName = storageClassName;
            this.Capacity = capacity;
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
        
        public string Status
        {
            get;
        }

        public string VolumeName
        {
            get;
        }

        public IReadOnlyList<string> AccessModes
        {
            get;
        }

        public string StorageClassName
        {
            get;
        }

        public List<Capacity> Capacity
        {
            get;
        }
    }
}
