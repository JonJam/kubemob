using System;
using System.Windows.Input;
using KubeMob.Common.Services.Settings;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels
{
    [Preserve(AllMembers = true)]
    public class AddAzureAccountViewModel : ViewModelBase
    {
        private readonly IAppSettings appSettings;

        public AddAzureAccountViewModel(IAppSettings appSettings)
        {
            this.appSettings = appSettings;

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

            // TODO move to service
            this.ViewInformationCommand = new Command(() => Device.OpenUri(this.appSettings.AzureHelpLink));
        }

        public ICommand ViewInformationCommand { get; }
    }
}