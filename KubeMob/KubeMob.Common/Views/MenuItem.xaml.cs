using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Views
{
    [Preserve(AllMembers = true)]
    public partial class MenuItem : ContentView
    {
        public static readonly BindableProperty TitleProperty = BindableProperty.CreateAttached(
            nameof(MenuItem.TitleProperty),
            typeof(string),
            typeof(MenuItem),
            string.Empty,
            propertyChanged: MenuItem.OnTitleChanged);

        public MenuItem() => this.InitializeComponent();

        public string Title
        {
            get => (string)this.GetValue(MenuItem.TitleProperty);
            set => this.SetValue(MenuItem.TitleProperty, value);
        }

        private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            MenuItem item = (MenuItem)bindable;

            item.TitleLabel.Text = newValue as string;
        }
    }
}