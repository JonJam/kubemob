using System.Collections.Generic;
using KubeMob.Common.Services.Kubernetes.Model.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class StatefulSetDetail : ObjectDetailBase
    {
        public StatefulSetDetail(
            string uid,
            string name,
            string namespaceName,
            IReadOnlyList<MetadataItem> labels,
            IReadOnlyList<MetadataItem> annotations,
            string creationTime,
            IReadOnlyList<string> images,
            string pods)
            : base(uid, name, namespaceName, labels, annotations, creationTime)
        {
            this.Images = images;
            this.Pods = pods;
        }

        public IReadOnlyList<string> Images
        {
            get;
        }

        public string Pods
        {
            get;
        }
    }
}
