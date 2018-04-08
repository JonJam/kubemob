using System;
using KubeMob.Common.Services.AccountManagement.Azure;

namespace KubeMob.Common.Services.Settings
{
    public interface IAppSettings
    {
        Uri AzureHelpLink { get; }

        AzureAccount AzureAccount { get; set; }
    }
}
