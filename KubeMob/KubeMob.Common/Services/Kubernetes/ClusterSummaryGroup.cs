using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes
{
    [Preserve(AllMembers = true)]
    public class ClusterSummaryGroup : List<ClusterSummary>
    {
        public ClusterSummaryGroup(
            string accountId,
            string title,
            Type accountManagerType)
        {
            this.AccountId = accountId;
            this.Title = title;
            this.AccountManagerType = accountManagerType;
        }

        public Type AccountManagerType { get; }

        public string AccountId { get; }

        public string Title { get; }

        public char ShortName => this.Title.First();

        public string ErrorMessage { get; set; }

        public bool HasErrorMessage => !string.IsNullOrWhiteSpace(this.ErrorMessage);

        public bool IsEmpty => !this.HasErrorMessage && this.Count == 0;
    }
}