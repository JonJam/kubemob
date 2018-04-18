using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.AccountManagement
{
    [Preserve(AllMembers = true)]
    public class ClusterGroup : List<Cluster>
    {
        public ClusterGroup(
            string accountId,
            AccountType accountType,
            string title)
        {
            this.AccountId = accountId;
            this.AccountType = accountType;
            this.Title = title;
        }

        public string AccountId { get; }

        public AccountType AccountType { get; }

        public string Title { get; }

        public char ShortName => this.Title.First();

        public string ErrorMessage { get; set; }

        public bool HasErrorMessage => !string.IsNullOrWhiteSpace(this.ErrorMessage);

        public bool IsEmpty => !this.HasErrorMessage && this.Count == 0;
    }
}