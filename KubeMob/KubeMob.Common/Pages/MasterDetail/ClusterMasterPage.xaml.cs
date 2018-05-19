using KubeMob.Common.Pages.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Pages.MasterDetail
{
    public partial class ClusterMasterPage : ExtendedContentPage
    {
        [Preserve]
        public ClusterMasterPage()
        {
            this.InitializeComponent();

            this.MenuListView = this.MenuItems;
        }

        public ListView MenuListView
        {
            get;
        }
    }
}