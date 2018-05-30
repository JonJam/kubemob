using KubeMob.Common.Pages.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Pages.Pods
{
    public partial class PodsPage : ExtendedContentPage
    {
        [Preserve]
        public PodsPage()
        {
            this.InitializeComponent();

            this.Pods.ItemSelected += this.OnPodSelected;
        }

        private void OnPodSelected(object sender, SelectedItemChangedEventArgs e) => this.Pods.SelectedItem = null;
    }
}