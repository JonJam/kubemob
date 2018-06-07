using KubeMob.Common.Pages.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Pages.StatefulSets
{
    public partial class StatefulSetsPage : ExtendedContentPage
    {
        [Preserve]
        public StatefulSetsPage()
        {
            this.InitializeComponent();
            this.StatefulSets.ItemSelected += this.OnStatefulSetSelected;
        }

        private void OnStatefulSetSelected(object sender, SelectedItemChangedEventArgs e) => this.StatefulSets.SelectedItem = null;
    }
}