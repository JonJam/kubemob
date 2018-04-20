using System.Collections.Generic;

namespace KubeMob.Common.Services.AccountManagement.Azure
{
    public class AzureAccount : CloudAccount
    {
        public AzureAccount(
            string id,
            IDictionary<string, string> properties)
            : base(id, properties)
        {
        }

        public AzureAccount(
            string name,
            string environmentId,
            string tenantId,
            string clientId,
            string clientSecret)
            : base(tenantId, name, CloudAccountType.Azure)
        {
            this.EnvironmentId = environmentId;
            this.ClientId = clientId;
            this.ClientSecret = clientSecret;
        }

        public string EnvironmentId
        {
            get => this.Properties[nameof(this.EnvironmentId)];
            set => this.Properties[nameof(this.EnvironmentId)] = value;
        }

        public string TenantId => this.Id;

        public string ClientId
        {
            get => this.Properties[nameof(this.ClientId)];
            set => this.Properties[nameof(this.ClientId)] = value;
        }

        public string ClientSecret
        {
            get => this.Properties[nameof(this.ClientSecret)];
            set => this.Properties[nameof(this.ClientSecret)] = value;
        }
    }
}
