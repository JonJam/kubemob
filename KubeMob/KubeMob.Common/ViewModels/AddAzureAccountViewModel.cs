using System.Windows.Input;
using KubeMob.Common.Services.AccountManagement;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels
{
    [Preserve(AllMembers = true)]
    public class AddAzureAccountViewModel : ViewModelBase
    {
        private readonly IAccountManager accountManager;

        public AddAzureAccountViewModel(IAccountManager accountManager)
        {
            this.accountManager = accountManager;

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
            
            this.ViewInformationCommand = new Command(() => this.accountManager.LaunchHelp());
        }

        public ICommand ViewInformationCommand { get; }
    }
}