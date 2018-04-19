using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace KubeMob.Common.Pages
{
    public partial class ClusterMasterDetailPage : MasterDetailPage
    {
        [Preserve]
        public ClusterMasterDetailPage()
        {
            this.InitializeComponent();

            // Support for iPhone X: https://blog.xamarin.com/making-ios-11-even-easier-xamarin-forms/
            this.On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            // TODO Try refactor this into ViewModel.??
            this.MasterPage.MenuListView.ItemSelected += this.OnMenuItemSelected;
        }

        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            this.IsPresented = false;

            this.MasterPage.MenuListView.SelectedItem = null;
        }
    }
}