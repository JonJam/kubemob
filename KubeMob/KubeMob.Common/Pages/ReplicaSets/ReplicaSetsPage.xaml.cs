using KubeMob.Common.Pages.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Pages.ReplicaSets
{
    public partial class ReplicaSetsPage : ExtendedContentPage
    {
        [Preserve]
        public ReplicaSetsPage()
        {
            this.InitializeComponent();
            this.ReplicaSets.ItemSelected += this.OnReplicaSetSelected;
        }

        private void OnReplicaSetSelected(object sender, SelectedItemChangedEventArgs e) => this.ReplicaSets.SelectedItem = null;
    }
}
