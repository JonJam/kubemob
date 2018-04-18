using System.Collections.Generic;
using System.Threading.Tasks;

namespace KubeMob.Common.Services.AccountManagement
{
    public interface IAccountManager
    {
        AccountType Key { get; }

        IList<CloudEnvironment> Environments { get; }

        void LaunchHelp();

        Task<IEnumerable<ClusterGroup>> GetClusters();

        void SetSelectedCluster(Cluster cluster);
        
        void RemoveAccount(string id);
    }
}
