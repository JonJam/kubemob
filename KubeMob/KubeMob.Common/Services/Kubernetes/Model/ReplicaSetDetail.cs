using System.Collections.Generic;
using KubeMob.Common.Services.Kubernetes.Model.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class ReplicaSetDetail : ObjectDetailBase
    {
        public ReplicaSetDetail(
            string uid,
            string name,
            string namespaceName,
            IReadOnlyList<MetadataItem> labels,
            IReadOnlyList<MetadataItem> annotations,
            string creationTime,
            IReadOnlyList<string> selectors,
            IReadOnlyList<string> images,
            string pods,
            string relatedSelector)
            : base(uid, name, namespaceName, labels, annotations, creationTime)
        {
            this.Selectors = selectors;
            this.Images = string.Join(", ", images);
            this.Pods = pods;
            this.RelatedSelector = relatedSelector;
        }

        public IReadOnlyList<string> Selectors
        {
            get;
        }

        public string Images
        {
            get;
        }

        public string Pods
        {
            get;
        }

        public string RelatedSelector
        {
            get;
        }
    }
}
