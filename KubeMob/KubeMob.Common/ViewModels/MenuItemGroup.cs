using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels
{
    [Preserve(AllMembers = true)]
    public class MenuItemGroup : List<MenuItemViewModel>
    {
        public MenuItemGroup(
            string title) => this.Title = title;

        public string Title { get; }

        public char ShortName => !string.IsNullOrWhiteSpace(this.Title) ? this.Title.First() : '\0';
    }
}
