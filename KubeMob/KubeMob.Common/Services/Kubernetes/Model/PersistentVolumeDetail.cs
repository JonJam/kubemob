using System.Collections.Generic;
using KubeMob.Common.Services.Kubernetes.Model.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class PersistentVolumeDetail : ObjectDetailBase
    {
        public PersistentVolumeDetail(
            string name,
            string namespaceName,
            IReadOnlyList<string> labels,
            IReadOnlyList<string> annotations,
            string creationTime)
            : base(name, namespaceName, labels, annotations, creationTime)
        {
        }

        // TODO add properties.
    }
}
