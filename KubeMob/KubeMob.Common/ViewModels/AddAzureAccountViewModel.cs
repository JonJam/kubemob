using System;
using System.Windows.Input;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels
{
    [Preserve(AllMembers = true)]
    public class AddAzureAccountViewModel : ViewModelBase
    {
        public AddAzureAccountViewModel()
        {


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

            // TODO move to service and settings.
            this.ViewInformationCommand = new Command(() =>
                Device.OpenUri(new Uri(
                    "https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-group-create-service-principal-portal")));
        }

        public ICommand ViewInformationCommand { get; }
    }
}