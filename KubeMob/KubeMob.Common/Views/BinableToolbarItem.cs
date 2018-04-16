using System.Collections.Generic;
using Xamarin.Forms;

namespace KubeMob.Common.Views
{
    // TODO Test
    public class BindableToolbarItem : ToolbarItem
    {
        public static readonly BindableProperty IsVisibleProperty = BindableProperty.CreateAttached(
            nameof(BindableToolbarItem.IsVisible),
            typeof(bool),
            typeof(BindableToolbarItem),
            true,
            propertyChanged: BindableToolbarItem.OnIsVisibleChanged);

        public bool IsVisible
        {
            get => (bool)this.GetValue(BindableToolbarItem.IsVisibleProperty);
            set => this.SetValue(BindableToolbarItem.IsVisibleProperty, value);
        }

        private static void OnIsVisibleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            BindableToolbarItem toolbarItem = (BindableToolbarItem)bindable;

            IList<ToolbarItem> toolbarItems = ((ContentPage)toolbarItem.Parent).ToolbarItems;

            bool isVisible = (bool)newValue;

            if (isVisible &&
                !toolbarItems.Contains(toolbarItem))
            {
                toolbarItems.Add(toolbarItem);
            }
            else if (!isVisible &&
                     toolbarItems.Contains(toolbarItem))
            {
                toolbarItems.Remove(toolbarItem);
            }
        }
    }
}
