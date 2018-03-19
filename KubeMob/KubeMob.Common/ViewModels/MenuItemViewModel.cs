using System.Windows.Input;

namespace KubeMob.Common.ViewModels
{
    public class MenuItemViewModel
    {
        public MenuItemViewModel(
            string title,
            ICommand command)
        {
            this.Title = title;
            this.Command = command;
        }

        public string Title { get; set; }

        public ICommand Command { get; set; }
    }
}
