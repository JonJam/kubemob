using KubeMob.Common.Pages.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Pages.Jobs
{
    public partial class JobsPage : ExtendedContentPage
    {
        [Preserve]
        public JobsPage(){
            this.InitializeComponent();
            this.Jobs.ItemSelected += this.OnDeploymentSelected;
        }

        private void OnDeploymentSelected(object sender, SelectedItemChangedEventArgs e) => this.Jobs.SelectedItem = null;
    }
}