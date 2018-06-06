using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class ReplicationControllerDetail
    {
        public ReplicationControllerDetail(
            string name,
            string namespaceName,
            IReadOnlyList<string> labels,
            IReadOnlyList<string> annotations,
            string creationTime,
            IReadOnlyList<string> selectors,
            IReadOnlyList<string> images,
            string pods)
        {
            this.Name = name;
            this.NamespaceName = namespaceName;
            this.Labels = labels;
            this.Annotations = annotations;
            this.CreationTime = creationTime;
            this.Selectors = selectors;
            this.Images = images;
            this.Pods = pods;
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
