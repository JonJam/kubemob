using System.Windows.Input;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels
{
    [Preserve(AllMembers = true)]
    public class MenuItemViewModel
    {
        public MenuItemViewModel(
            string title,
            ICommand command)
        {
            this.Title = title;
            this.Command = command;
        }

        public string Title { get; }

        public ICommand Command { get; }
    }
}
