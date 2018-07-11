using System.Collections.Generic;
using KubeMob.Common.Services.Kubernetes.Model.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class IngressDetail : ObjectDetailBase
    {
        public IngressDetail(
            string uid,
            string name,
            string namespaceName,
            IReadOnlyList<MetadataItem> labels,
            IReadOnlyList<MetadataItem> annotations,
            string creationTime)
            : base(uid, name, namespaceName, labels, annotations, creationTime)
        {
        }
    }
}
