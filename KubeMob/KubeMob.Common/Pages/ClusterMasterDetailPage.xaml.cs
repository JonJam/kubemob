using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Pages
{
    public partial class ClusterMasterDetailPage : MasterDetailPage
    {
        [Preserve]
        public ClusterMasterDetailPage()
        {
            this.InitializeComponent();

            this.MasterPage.MenuListView.ItemSelected += this.OnMenuItemSelected;
        }

        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            this.IsPresented = false;

            this.MasterPage.MenuListView.SelectedItem = null;
        }
    }
}