using KubeMob.Common.Services.AccountManagement.Model;

namespace KubeMob.Common.Services.AccountManagement.Azure.Model
{
    public class AzureAccount : CloudAccount
    {
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
            get;
        }

        public string TenantId => this.Id;

        public string ClientId
        {
            get;
        }

        public string ClientSecret
        {
            get;
        }
    }
}
