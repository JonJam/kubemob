using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class DeploymentDetail
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
            string status
        )
        {
            this.Name = name;
            this.NamespaceName = namespaceName;
            this.Labels = labels;
            this.Annotations = annotations;
            this.CreationTime = creationTime;
            this.Selectors = selectors;
            this.Strategy = strategy;
            this.MinReadySeconds = minReadySeconds;
            this.RevisionHistoryLimit = revisionHistoryLimit;
            this.RollingUpdateStrategy = rollingUpdateStrategy;
            this.Status = status;
        }

        public string Name
        {
            get;
        }

        public string NamespaceName
        {
            get;
        }

        public IReadOnlyList<string> Labels
        {
            get;
        }

        public IReadOnlyList<string> Annotations
        {
            get;
        }

        public string CreationTime
        {
            get;
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
