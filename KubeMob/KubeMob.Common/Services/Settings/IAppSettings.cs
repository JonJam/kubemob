using System;
using System.Collections.Generic;
using KubeMob.Common.Services.AccountManagement.Azure;

namespace KubeMob.Common.Services.Settings
{
    public interface IAppSettings
    {
        Uri AzureHelpLink { get; }

        List<AzureAccount> GetAzureAccounts();

        void SetAzureAccounts(List<AzureAccount> accounts);
    }
}
