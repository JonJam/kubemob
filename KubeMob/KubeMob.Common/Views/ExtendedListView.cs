using System.Globalization;
using System.Windows.Input;
using KubeMob.Common.Converters;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Views
{
    [Preserve(AllMembers = true)]
    public class ExtendedListView : ListView
    {
        public static readonly BindableProperty ItemTappedCommandProperty = BindableProperty.CreateAttached(
            nameof(ExtendedListView.ItemTappedCommand),
            typeof(ICommand),
            typeof(ExtendedListView),
            null);

        // Performance improvements for ListView: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/listview/performance
        public ExtendedListView()
            : base(ListViewCachingStrategy.RecycleElement)
        {
            this.ItemSelected += this.OnItemSelected;
            this.ItemTapped += this.OnItemTapped;
        }

        public ICommand ItemTappedCommand
        {
            get => (ICommand)this.GetValue(ExtendedListView.ItemTappedCommandProperty);
            set => this.SetValue(ExtendedListView.ItemTappedCommandProperty, value);
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e) => this.SelectedItem = null;

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (this.ItemTappedCommand == null)
            {
                return;
            }

            ItemTappedEventArgsConverter converter = (ItemTappedEventArgsConverter)Application.Current
                .Resources[nameof(ItemTappedEventArgsConverter)];
            object parameter = converter.Convert(
                e,
                typeof(object),
                null,
                CultureInfo.CurrentUICulture);

            if (this.ItemTappedCommand.CanExecute(parameter))
            {
                this.ItemTappedCommand.Execute(parameter);
            }
        }
    }
}
