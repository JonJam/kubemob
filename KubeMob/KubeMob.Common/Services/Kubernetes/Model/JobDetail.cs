using System.Collections.Generic;
using KubeMob.Common.Services.Kubernetes.Model.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class JobDetail : ObjectDetailBase
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
            : base(name, namespaceName, labels, annotations, creationTime)
        {
            this.Images = images;
            this.Parallelism = parallelism;
            this.Completions = completions;
            this.Status = status;
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
