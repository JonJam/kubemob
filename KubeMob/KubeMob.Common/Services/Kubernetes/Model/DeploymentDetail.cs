using System.Collections.Generic;
using KubeMob.Common.Services.Kubernetes.Model.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class DeploymentDetail : ObjectDetailBase
    {
        public DeploymentDetail(
            string name,
            string namespaceName,
            IReadOnlyList<string> labels,
            IReadOnlyList<string> annotations,
            string creationTime,
            IReadOnlyList<string> selectors,
            string strategy,
            int minReadySeconds,
            string revisionHistoryLimit,
            string rollingUpdateStrategy,
            string status)
            : base(name, namespaceName, labels, annotations, creationTime)
        {
            this.Selectors = selectors;
            this.Strategy = strategy;
            this.MinReadySeconds = minReadySeconds;
            this.RevisionHistoryLimit = revisionHistoryLimit;
            this.RollingUpdateStrategy = rollingUpdateStrategy;
            this.Status = status;
        }

        public IReadOnlyList<string> Selectors
        {
            get;
        }

        public string Strategy
        {
            get;
        }

        public int MinReadySeconds
        {
            get;
        }

        public string RevisionHistoryLimit
        {
            get;
        }

        public string RollingUpdateStrategy
        {
            get;
        }

        public string Status
        {
            get;
        }
    }
}
