namespace KubeMob.Common.Services.AccountManagement.Azure
{
    public interface IAzureAccountManager : IAccountManager
    {
        (bool isValid, string message) IsValidCredentials(
            CloudEnvironment cloudEnvironment,
            string tenantId,
            string clientId,
            string clientSecret);

        void AddCredentials(
            CloudEnvironment cloudEnvironment,
            string tenantId,
            string clientId,
            string clientSecret);
    }
}
