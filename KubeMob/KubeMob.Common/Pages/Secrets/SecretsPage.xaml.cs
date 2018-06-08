using KubeMob.Common.Pages.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Pages.Secrets
{
    public partial class SecretsPage : ExtendedContentPage
    {
        [Preserve]
        public SecretsPage()
        {
            this.InitializeComponent();

            this.Secrets.ItemSelected += this.OnSecretSelected;
        }

        private void OnSecretSelected(object sender, SelectedItemChangedEventArgs e) => this.Secrets.SelectedItem = null;
    }
}