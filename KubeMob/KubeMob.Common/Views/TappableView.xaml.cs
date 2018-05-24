using System;
using System.Windows.Input;
using KubeMob.Common.Behaviors;
using KubeMob.Common.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Views
{
    [Preserve(AllMembers = true)]
    public partial class TappableView : ContentView
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.CreateAttached(
            nameof(TappableView.Command),
            typeof(ICommand),
            typeof(TappableView),
            null);

        private readonly Color originalBackgroundColor;

        public TappableView()
        {
            this.InitializeComponent();

            this.originalBackgroundColor = this.BackgroundColor;
        }

        public ICommand Command
        {
            get => (ICommand)this.GetValue(TappableView.CommandProperty);
            set => this.SetValue(TappableView.CommandProperty, value);
        }

        private async void HandleTap(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.LightGray;

            this.Command?.Execute(e);

            await this.ColorTo(this.BackgroundColor, this.originalBackgroundColor, c => this.BackgroundColor = c);
        }
    }
}