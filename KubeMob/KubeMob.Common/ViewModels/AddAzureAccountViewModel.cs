using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels
{
    [Preserve(AllMembers = true)]
    public class AddAzureAccountViewModel : ViewModelBase
    {
        public AddAzureAccountViewModel()
        {
        }

        //////Using Azure client to get context.
        //var credentials = SdkContext.AzureCredentialsFactory.FromServicePrincipal(
        //    "",
        //    "",
        //    "e",
        //    AzureEnvironment.AzureGlobalCloud);

        //string aksId = "";

        //IAzure a = Azure.Authenticate(credentials).WithDefaultSubscription();

        //IKubernetesCluster kubernetesCluster = a.KubernetesClusters.GetByResourceGroup("", aksId);
        //var b = kubernetesCluster.UserKubeConfigContent;
    }
}