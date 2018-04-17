namespace KubeMob.Common.Services.AccountManagement.Azure
{
    public interface IAzureAccountManager : IAccountManager
    {
        (bool isValid, string message) TrySaveCredentials(
            CloudEnvironment cloudEnvironment,
            string tenantId,
            string clientId,
            string clientSecret,
            bool isEditing);

        AzureAccount GetAccount(string id);
    }
}
