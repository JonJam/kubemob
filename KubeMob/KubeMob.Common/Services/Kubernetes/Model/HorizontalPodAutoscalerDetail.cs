using System.Collections.Generic;
using KubeMob.Common.Services.Kubernetes.Model.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class HorizontalPodAutoscalerDetail : ObjectDetailBase
    {
        public HorizontalPodAutoscalerDetail(
            string name,
            string namespaceName,
            IReadOnlyList<string> labels,
            IReadOnlyList<string> annotations,
            string creationTime)
            : base(name, namespaceName, labels, annotations, creationTime)
        {
        }
    }
}
