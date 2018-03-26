
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Pages
{
    public partial class ClusterMasterPage : ContentPage
    {
        [Preserve]
        public ClusterMasterPage()
        {
            this.InitializeComponent();

            this.MenuListView = this.MenuItems;
        }

        public ListView MenuListView { get; }
    }
}