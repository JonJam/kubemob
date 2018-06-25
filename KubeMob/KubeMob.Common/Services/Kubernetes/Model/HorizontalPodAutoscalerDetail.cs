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
            string creationTime,
            ObjectReference target,
            int minReplicas,
            int maxReplicas,
            string targetCpuUtilization,
            int currentReplicas,
            int desiredReplicas,
            string currentCpuUtilization,
            string lastScaled)
            : base(name, namespaceName, labels, annotations, creationTime)
        {
            this.Target = target;
            this.MinReplicas = minReplicas;
            this.MaxReplicas = maxReplicas;
            this.TargetCpuUtilization = targetCpuUtilization;
            this.CurrentReplicas = currentReplicas;
            this.DesiredReplicas = desiredReplicas;
            this.CurrentCpuUtilization = currentCpuUtilization;
            this.LastScaled = lastScaled;
        }

        public ObjectReference Target
        {
            get;
        }

        public int MinReplicas
        {
            get;
        }

        public int MaxReplicas
        {
            get;
        }

        public string TargetCpuUtilization
        {
            get;
        }

        public int CurrentReplicas
        {
            get;
        }

        public int DesiredReplicas
        {
            get;
        }

        public string CurrentCpuUtilization
        {
            get;
        }

        public string LastScaled
        {
            get;
        }
    }
}
