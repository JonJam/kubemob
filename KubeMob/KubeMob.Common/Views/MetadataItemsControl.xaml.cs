using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Views
{
    [Preserve(AllMembers = true)]
    public partial class MetadataItemsControl : ItemsControl
    {
        public static readonly BindableProperty PopupCommandProperty = BindableProperty.CreateAttached(
            nameof(MetadataItemsControl.PopupCommandProperty),
            typeof(ICommand),
            typeof(MetadataItemsControl),
            null);

        public MetadataItemsControl() => this.InitializeComponent();

        public ICommand PopupCommand
        {
            get => (ICommand)this.GetValue(MetadataItemsControl.PopupCommandProperty);
            set => this.SetValue(MetadataItemsControl.PopupCommandProperty, value);
        }

        private void OnItemTapped(object sender, EventArgs e)
        {
            if (this.PopupCommand == null)
            {
                return;
            }

            TappedEventArgs args = (TappedEventArgs)e;

            if (this.PopupCommand.CanExecute(args.Parameter))
            {
                this.PopupCommand.Execute(args.Parameter);
            }
        }
    }
}