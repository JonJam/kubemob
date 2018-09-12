using System.Collections.Generic;
using KubeMob.Common.Services.Kubernetes.Model.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class JobDetail : ObjectDetailBase
    {
        public JobDetail(
            string uid,
            string name,
            string namespaceName,
            IReadOnlyList<MetadataItem> labels,
            IReadOnlyList<MetadataItem> annotations,
            string creationTime,
            string images,
            int parallelism,
            int completions,
            string status)
            : base(uid, name, namespaceName, labels, annotations, creationTime)
        {
            this.Images = images;
            this.Parallelism = parallelism;
            this.Completions = completions;
            this.Status = status;
        }

        public string Status
        {
            get;
        }

        public string Images
        {
            get;
        }

        public int Parallelism
        {
            get;
        }

        public int Completions
        {
            get;
        }
    }
}
