using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.MasterDetail
{
    [Preserve(AllMembers = true)]
    public class MenuItemGroup : List<IMenuItem>
    {
        public MenuItemGroup(
            string title) => this.Title = title;

        public string Title { get; }
    }
}
