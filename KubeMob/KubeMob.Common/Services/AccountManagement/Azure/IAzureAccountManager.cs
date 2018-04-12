namespace KubeMob.Common.Services.AccountManagement.Azure
{
    public interface IAzureAccountManager : IAccountManager
    {
        (bool isValid, string message) TryAddCredentials(
                    CloudEnvironment cloudEnvironment,
            string tenantId,
            string clientId,
            string clientSecret);
    }
}
