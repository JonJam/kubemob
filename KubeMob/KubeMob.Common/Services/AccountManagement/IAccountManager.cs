using System.Collections.Generic;
using System.Threading.Tasks;

namespace KubeMob.Common.Services.AccountManagement
{
    public interface IAccountManager
    {
        CloudAccountType Key { get; }

        IList<CloudEnvironment> Environments { get; }

        void LaunchHelp();

        Task<IEnumerable<ClusterGroup>> GetClusters();

        void SetSelectedCluster(Cluster cluster);

        Task RemoveAccount(string id);
    }
}
