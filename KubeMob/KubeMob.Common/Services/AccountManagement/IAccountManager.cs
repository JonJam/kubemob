using System.Collections.Generic;
using System.Threading.Tasks;
using KubeMob.Common.Services.Kubernetes;

namespace KubeMob.Common.Services.AccountManagement
{
    public interface IAccountManager
    {
        IList<CloudEnvironment> Environments { get; }

        void LaunchHelp();

        Task<IEnumerable<ClusterSummaryGroup>> GetClusters();

        void RemoveAccount(string id);
    }
}
