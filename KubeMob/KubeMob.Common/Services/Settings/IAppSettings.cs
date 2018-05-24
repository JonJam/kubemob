using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KubeMob.Common.Services.AccountManagement.Model;

namespace KubeMob.Common.Services.Settings
{
    public interface IAppSettings
    {
        Cluster SelectedCluster { get; set; }

        string SelectedNamespace
        {
            get;
            set;
        }

        bool ShowCronJobs
        {
            get;
            set;
        }

        bool ShowDaemonSets
        {
            get;
            set;
        }

        bool ShowDeployments
        {
            get;
            set;
        }

        bool ShowJobs
        {
            get;
            set;
        }

        bool ShowPods
        {
            get;
            set;
        }

        bool ShowReplicaSets
        {
            get;
            set;
        }

        bool ShowReplicationControllers
        {
            get;
            set;
        }

        bool ShowStatefulSets
        {
            get;
            set;
        }

        bool ShowIngresses
        {
            get;
            set;
        }

        bool ShowServices
        {
            get;
            set;
        }

        bool ShowConfigMaps
        {
            get;
            set;
        }

        bool ShowPersistentVolumeClaims
        {
            get;
            set;
        }

        bool ShowSecrets
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
