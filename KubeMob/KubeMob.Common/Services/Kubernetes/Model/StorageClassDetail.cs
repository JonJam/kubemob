using System.Collections.Generic;
using KubeMob.Common.Services.Kubernetes.Model.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class StorageClassDetail : ObjectDetailBase
    {
        public StorageClassDetail(
            string uid,
            string name,
            string namespaceName,
            IReadOnlyList<string> labels,
            IReadOnlyList<string> annotations,
            string creationTime,
            string provisioner,
            IReadOnlyList<string> parameters)
            : base(uid, name, namespaceName, labels, annotations, creationTime)
        {
            this.Provisioner = provisioner;
            this.Parameters = parameters;
        }

        public string Provisioner
        {
            get;
        }

        public IReadOnlyList<string> Parameters
        {
            get;
        }
    }
}
