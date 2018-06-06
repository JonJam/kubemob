using KubeMob.Common.Pages.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Pages.ReplicationControllers
{
    public partial class ReplicationControllersPage : ExtendedContentPage
    {
        [Preserve]
        public ReplicationControllersPage()
        {
            this.InitializeComponent();

            this.ReplicationControllers.ItemSelected += this.OnReplicationControllerSelected;
        }

        private void OnReplicationControllerSelected(object sender, SelectedItemChangedEventArgs e) => this.ReplicationControllers.SelectedItem = null;
    }
}