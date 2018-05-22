using System.Windows.Input;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.MasterDetail
{
    [Preserve(AllMembers = true)]
    public class SettingsMenuItemViewModel : ViewModelBase, IMenuItem
    {
        public SettingsMenuItemViewModel(
            string title,
            ICommand command)
        {
            this.Title = title;
            this.Command = command;
        }

        public string Title
        {
            get;
        }
        public ICommand Command
        {
            get;
        }
    }
}
