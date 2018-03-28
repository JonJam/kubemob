using System.Collections.Generic;

namespace KubeMob.Common.Services.AccountManagement
{
    public interface IAccountManager
    {
        IList<CloudEnvironment> Environments { get; }

        void LaunchHelp();
    }
}
