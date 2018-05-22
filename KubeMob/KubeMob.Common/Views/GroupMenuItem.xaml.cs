using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Views
{
    [Preserve(AllMembers = true)]
    public partial class GroupMenuItem : ContentView
    {
        public static readonly BindableProperty TitleProperty = BindableProperty.CreateAttached(
            nameof(GroupMenuItem.TitleProperty),
            typeof(string),
            typeof(GroupMenuItem),
            string.Empty,
            propertyChanged: GroupMenuItem.OnTitleChanged);

        public GroupMenuItem() => this.InitializeComponent();

        public string Title
        {
            get => (string)this.GetValue(GroupMenuItem.TitleProperty);
            set => this.SetValue(GroupMenuItem.TitleProperty, value);
        }

        private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            GroupMenuItem item = (GroupMenuItem)bindable;

            item.TitleLabel.Text = newValue as string;
        }
    }
}