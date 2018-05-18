using Newtonsoft.Json;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.AccountManagement.Model
{
    [Preserve(AllMembers = true)]
    public class Cluster
    {
        public Cluster(
            string id,
            string name,
            string accountId,
            CloudAccountType accountType)
        {
            this.Id = id;
            this.Name = name;
            this.AccountId = accountId;
            this.AccountType = accountType;
        }

        [JsonProperty("id")]
        public string Id { get; }

        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("accountId")]
        public string AccountId { get; }

        [JsonProperty("accountType")]
        public CloudAccountType AccountType { get; }
    }
}
