using KubeMob.Common.Pages.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Pages.DaemonSets
{
    public partial class DaemonSetsPage : ExtendedContentPage
    {
        [Preserve]
        public DaemonSetsPage()
        {
            this.InitializeComponent();
            this.DaemonSets.ItemSelected += this.OnCronJobSelected;
        }

        private void OnCronJobSelected(object sender, SelectedItemChangedEventArgs e) => this.DaemonSets.SelectedItem = null;
    }
}