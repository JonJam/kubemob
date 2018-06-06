using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class JobDetail
    {
        public JobDetail(
            string name,
            string namespaceName,
            IReadOnlyList<string> labels,
            IReadOnlyList<string> annotations,
            string creationTime,
            IReadOnlyList<string> images,
            int parallelism,
            int completions,
            string status)
        {
            this.Name = name;
            this.NamespaceName = namespaceName;
            this.Labels = labels;
            this.Annotations = annotations;
            this.CreationTime = creationTime;
            this.Images = images;
            this.Parallelism = parallelism;
            this.Completions = completions;
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

        // TODO Handle links
        public IReadOnlyList<string> Annotations
        {
            get;
        }

        public string CreationTime
        {
            get;
        }

        public string Status
        {
            get;
        }

        public IReadOnlyList<string> Images
        {
            get;
        }

        public int Parallelism
        {
            get;
        }

        public int Completions
        {
            get;
        }
    }
}
