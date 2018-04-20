using System.Threading.Tasks;

namespace KubeMob.Common.Services.AccountManagement.Azure
{
    public interface IAzureAccountManager : IAccountManager
    {
        Task<(bool isValid, string message)> TrySaveCredentials(
            CloudEnvironment cloudEnvironment,
            string tenantId,
            string clientId,
            string clientSecret,
            bool isEditing);

        Task<AzureAccount> GetAccount(string id);
    }
}
