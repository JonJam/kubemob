using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Views
{
    [Preserve(AllMembers = true)]
    public partial class SettingsItemViewCell : ViewCell
    {
        public static readonly BindableProperty IndicatorProperty = BindableProperty.CreateAttached(
            nameof(SettingsItemViewCell.IndicatorProperty),
            typeof(string),
            typeof(SettingsItemViewCell),
            null,
            propertyChanged: SettingsItemViewCell.OnIndicatorChanged);

        public static readonly BindableProperty IconProperty = BindableProperty.CreateAttached(
            nameof(SettingsItemViewCell.IconProperty),
            typeof(string),
            typeof(SettingsItemViewCell),
            null,
            propertyChanged: SettingsItemViewCell.OnIconChanged);

        public static readonly BindableProperty TextProperty = BindableProperty.CreateAttached(
            nameof(SettingsItemViewCell.TextProperty),
            typeof(string),
            typeof(SettingsItemViewCell),
            null,
            propertyChanged: SettingsItemViewCell.OnTextChanged);

        public SettingsItemViewCell() => this.InitializeComponent();

        public string Indicator
        {
            get => (string)this.GetValue(SettingsItemViewCell.IndicatorProperty);
            set => this.SetValue(SettingsItemViewCell.IndicatorProperty, value);
        }

        public string Icon
        {
            get => (string)this.GetValue(SettingsItemViewCell.IconProperty);
            set => this.SetValue(SettingsItemViewCell.IconProperty, value);
        }

        public string Text
        {
            get => (string)this.GetValue(SettingsItemViewCell.TextProperty);
            set => this.SetValue(SettingsItemViewCell.TextProperty, value);
        }

        private static void OnIndicatorChanged(
            BindableObject bindable,
            object oldValue,
            object newValue)
        {
            SettingsItemViewCell item = (SettingsItemViewCell)bindable;

            item.StyleId = newValue as string;
        }

        private static void OnIconChanged(
            BindableObject bindable,
            object oldValue,
            object newValue)
        {
            SettingsItemViewCell item = (SettingsItemViewCell)bindable;

            item.IconCachedImage.Source = ImageSource.FromFile(newValue as string);
        }

        private static void OnTextChanged(
            BindableObject bindable,
            object oldValue,
            object newValue)
        {
            SettingsItemViewCell item = (SettingsItemViewCell)bindable;

            item.TextLabel.Text = newValue as string;
        }
    }
}