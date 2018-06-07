using KubeMob.Common.Pages.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Pages.ConfigMaps
{
    public partial class ConfigMapsPage : ExtendedContentPage
    {
        [Preserve]
        public ConfigMapsPage()
        {
            this.InitializeComponent();

            this.ConfigMaps.ItemSelected += this.OnConfigMapSelected;
        }

        private void OnConfigMapSelected(object sender, SelectedItemChangedEventArgs e) => this.ConfigMaps.SelectedItem = null;
    }
}