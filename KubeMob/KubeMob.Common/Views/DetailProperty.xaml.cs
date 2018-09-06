using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Views
{
    [Preserve(AllMembers = true)]
    public partial class DetailProperty : ContentView
    {
        public static readonly BindableProperty LabelProperty = BindableProperty.CreateAttached(
            nameof(DetailProperty.LabelProperty),
            typeof(string),
            typeof(DetailProperty),
            string.Empty,
            propertyChanged: DetailProperty.OnLabelChanged);

        public static readonly BindableProperty ValueProperty = BindableProperty.CreateAttached(
            nameof(DetailProperty.ValueProperty),
            typeof(string),
            typeof(DetailProperty),
            string.Empty,
            propertyChanged: DetailProperty.OnValueChanged);

        public DetailProperty() => this.InitializeComponent();

        public string Label
        {
            get => (string)this.GetValue(DetailProperty.LabelProperty);
            set => this.SetValue(DetailProperty.LabelProperty, value);
        }

        public string Value
        {
            get => (string)this.GetValue(DetailProperty.ValueProperty);
            set => this.SetValue(DetailProperty.ValueProperty, value);
        }

        private static void OnLabelChanged(
            BindableObject bindable,
            object oldValue,
            object newValue)
        {
            DetailProperty item = (DetailProperty)bindable;

            item.PropertyLabelLabel.Text = newValue as string;
        }

        private static void OnValueChanged(
            BindableObject bindable,
            object oldValue,
            object newValue)
        {
            DetailProperty item = (DetailProperty)bindable;

            item.PropertyValueLabel.Text = newValue as string;
        }
    }
}