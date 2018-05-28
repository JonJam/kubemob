using System.Collections.Generic;
using System.Threading.Tasks;
using KubeMob.Common.Services.AccountManagement.Model;

namespace KubeMob.Common.Services.AccountManagement
{
    public interface IAccountManager
    {
        CloudAccountType Key { get; }

        IList<CloudEnvironment> Environments { get; }

        void LaunchHelp();

        Task RemoveAccount(string id);

        Task<IEnumerable<ClusterGroup>> GetClusters();

        void SetSelectedCluster(Cluster cluster);

        void RemoveSelectedCluster();

        Task<byte[]> GetSelectedClusterKubeConfigContent();
    }
}
