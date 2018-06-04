using KubeMob.Common.Pages.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Pages.Deployments
{
    public partial class DeploymentsPage : ExtendedContentPage
    {
        [Preserve]
        public DeploymentsPage()
        {
            this.InitializeComponent();
            this.Deployments.ItemSelected += this.OnDeploymentSelected;
        }

        private void OnDeploymentSelected(object sender, SelectedItemChangedEventArgs e) => this.Deployments.SelectedItem = null;
    }
}