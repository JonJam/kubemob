using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes
{
    [Preserve(AllMembers = true)]
    public class ClusterSummaryGroup : List<ClusterSummary>
    {
        public ClusterSummaryGroup(
            string title) => this.Title = title;

        public string Title { get; }

        public char ShortName => this.Title.First();
    }
}
