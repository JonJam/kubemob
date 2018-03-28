using System.Collections.Generic;
using KubeMob.Common.Resx;
using KubeMob.Common.Services.Settings;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.AccountManagement
{
    public class AzureAccountManager : IAccountManager
    {
        private readonly IAppSettings appSettings;

        [Preserve]
        public AzureAccountManager(IAppSettings appSettings)
        {
            this.appSettings = appSettings;

            this.Environments = new List<CloudEnvironment>()
            {
                new CloudEnvironment(
                    AzureEnvironment.AzureChinaCloud.ToString(),
                    AppResources.AzureEnvironment_China),

                new CloudEnvironment(
                    AzureEnvironment.AzureGermanCloud.ToString(),
                    AppResources.AzureEnvironment_German),

                new CloudEnvironment(
                    AzureEnvironment.AzureGlobalCloud.ToString(),
                    AppResources.AzureEnvironment_Global,
                    true),

                new CloudEnvironment(
                    AzureEnvironment.AzureUSGovernment.ToString(),
                    AppResources.AzureEnvironment_USGovernment)
            };
        }

        public IList<CloudEnvironment> Environments { get; }

        public void LaunchHelp() => Device.OpenUri(this.appSettings.AzureHelpLink);


        ////Using Azure client to get context.
        // var credentials = SdkContext.AzureCredentialsFactory.FromServicePrincipal(
        //    "",
        //    "",
        //    "e",
        //    AzureEnvironment.);

        // string aksId = "";

        // IAzure a = Azure.Authenticate(credentials).WithDefaultSubscription();

        // IKubernetesCluster kubernetesCluster = a.KubernetesClusters.GetByResourceGroup("", aksId);
        // var b = kubernetesCluster.UserKubeConfigContent;
    }
}