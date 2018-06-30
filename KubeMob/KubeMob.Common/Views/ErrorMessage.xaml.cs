using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Views
{
    [Preserve(AllMembers = true)]
    public partial class ErrorMessage : ContentView
    {
        public static readonly BindableProperty MessageProperty = BindableProperty.CreateAttached(
            nameof(ErrorMessage.MessageProperty),
            typeof(string),
            typeof(ErrorMessage),
            string.Empty,
            propertyChanged: ErrorMessage.OnMessageChanged);

        public ErrorMessage() => this.InitializeComponent();

        public string Message
        {
            get => (string)this.GetValue(ErrorMessage.MessageProperty);
            set => this.SetValue(ErrorMessage.MessageProperty, value);
        }

        private static void OnMessageChanged(
            BindableObject bindable,
            object oldValue,
            object newValue)
        {
            ErrorMessage item = (ErrorMessage)bindable;

            item.MessageLabel.Text = newValue as string;
        }
    }
}