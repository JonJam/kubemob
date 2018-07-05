using System.Collections.Generic;
using KubeMob.Common.Services.Kubernetes.Model.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class SecretDetail : ObjectDetailBase
    {
        public SecretDetail(
            string uid,
            string name,
            string namespaceName,
            IReadOnlyList<string> labels,
            IReadOnlyList<string> annotations,
            string creationTime,
            string type,
            IReadOnlyList<SecretData> data)
            : base(uid, name, namespaceName, labels, annotations, creationTime)
        {
            this.Type = type;
            this.Data = data;
        }

        public string Type
        {
            get;
        }

        public IReadOnlyList<SecretData> Data
        {
            get;
        }
    }
}
