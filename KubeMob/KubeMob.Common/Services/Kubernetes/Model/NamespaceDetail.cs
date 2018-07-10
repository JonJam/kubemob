using System.Collections.Generic;
using KubeMob.Common.Services.Kubernetes.Model.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class NamespaceDetail : ObjectDetailBase
    {
        public NamespaceDetail(
            string uid,
            string name,
            string namespaceName,
            IReadOnlyList<string> labels,
            IReadOnlyList<MetadataItem> annotations,
            string creationTime,
            string status)
            : base(uid, name, namespaceName, labels, annotations, creationTime) => this.Status = status;

        public string Status
        {
            get;
        }
    }
}
