using KubeMob.Common.Pages.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Pages.Ingresses
{
    public partial class IngressesPage : ExtendedContentPage
    {
        [Preserve]
        public IngressesPage()
        {
            this.InitializeComponent();

            this.Ingresses.ItemSelected += this.OnIngressSelected;
        }

        private void OnIngressSelected(object sender, SelectedItemChangedEventArgs e) => this.Ingresses.SelectedItem = null;
    }
}