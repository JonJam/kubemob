using KubeMob.Common.Pages.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Pages.PersistentVolumeClaims
{
    public partial class PersistentVolumeClaimsPage : ExtendedContentPage
    {
        [Preserve]
        public PersistentVolumeClaimsPage(){
            this.InitializeComponent();
            this.PersistentVolumeClaims.ItemSelected += this.OnPersistentVolumeClaimSelected;
        }

        private void OnPersistentVolumeClaimSelected(object sender, SelectedItemChangedEventArgs e) => this.PersistentVolumeClaims.SelectedItem = null;
    }
}