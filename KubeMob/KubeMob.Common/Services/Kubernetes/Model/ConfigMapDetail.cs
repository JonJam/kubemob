using System.Collections.Generic;
using KubeMob.Common.Services.Kubernetes.Model.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class ConfigMapDetail : ObjectDetailBase
    {
        public ConfigMapDetail(
            string uid,
            string name,
            string namespaceName,
            IReadOnlyList<string> labels,
            IReadOnlyList<MetadataItem> annotations,
            string creationTime,
            IReadOnlyList<KeyValuePair<string, string>> data)
            : base(uid, name, namespaceName, labels, annotations, creationTime) => this.Data = data;

        public IReadOnlyList<KeyValuePair<string, string>> Data
        {
            get;
        }
    }
}
