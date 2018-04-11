using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Pages
{
    public partial class ClustersPage : ContentPage
    {
        [Preserve]
        public ClustersPage()
        {
            this.InitializeComponent();

            // TODO Try refactor this into ViewModel.
            this.Clusters.ItemSelected += this.OnClusterSelected;
        }

        private void OnClusterSelected(object sender, SelectedItemChangedEventArgs e) => this.Clusters.SelectedItem = null;
    }
}