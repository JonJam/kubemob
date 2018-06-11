using System.Collections.Generic;
using KubeMob.Common.Services.Kubernetes.Model.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class DaemonSetDetail : ObjectDetailBase
    {
        public DaemonSetDetail(
            string name,
            string namespaceName,
            IReadOnlyList<string> labels,
            IReadOnlyList<string> annotations,
            string creationTime,
            IReadOnlyList<string> images,
            string pods)
            : base(name, namespaceName, labels, annotations, creationTime)
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
