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

        public TappableView() => this.InitializeComponent();

        public ICommand Command
        {
            get => (ICommand)this.GetValue(EventToCommandBehavior.CommandProperty);
            set => this.SetValue(EventToCommandBehavior.CommandProperty, value);
        }

        private async void HandleTap(object sender, EventArgs e)
        {
            Color originalBackgroundColor = this.BackgroundColor;

            this.BackgroundColor = Color.LightGray;

            this.Command?.Execute(e);

            await this.ColorTo(this.BackgroundColor, originalBackgroundColor, c => this.BackgroundColor = c);
        }
    }
}