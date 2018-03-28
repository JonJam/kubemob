using KubeMob.Common.Services.Settings;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.AccountManagement
{
    public class AzureAccountManager : IAccountManager
    {
        private readonly IAppSettings appSettings;

        [Preserve]
        public AzureAccountManager(IAppSettings appSettings) => this.appSettings = appSettings;

        public void LaunchHelp() => Device.OpenUri(this.appSettings.AzureHelpLink);
    }
}