using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KubeMob.Common.Services.AccountManagement.Model;

namespace KubeMob.Common.Services.Settings
{
    public interface IAppSettings
    {
        Uri AzureHelpLink { get; }

        Cluster SelectedCluster { get; set; }

        string SelectedNamespace
        {
            get;
            set;
        }

        Task<IEnumerable<T>> GetCloudAccounts<T>(
            CloudAccountType accountType)
            where T : CloudAccount;

        Task AddOrUpdateCloudAccount(CloudAccount cloudAccount);

        Task RemoveCloudAccount(string id);
    }
}
