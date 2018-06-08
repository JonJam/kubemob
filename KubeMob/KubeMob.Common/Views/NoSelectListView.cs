using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Views
{
    [Preserve(AllMembers = true)]
    public class NoSelectListView : ListView
    {
        public NoSelectListView() => this.ItemSelected += this.OnItemSelected;

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e) => this.SelectedItem = null;
    }
}
