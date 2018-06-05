using KubeMob.Common.Pages.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Pages.Services
{
    public partial class ServicesPage : ExtendedContentPage
    {
        [Preserve]
        public ServicesPage() {
            this.InitializeComponent();
            this.Services.ItemSelected += this.OnReplicaSetSelected;
        }

        private void OnReplicaSetSelected(object sender, SelectedItemChangedEventArgs e) => this.Services.SelectedItem = null;
    }
}