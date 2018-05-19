using System.Windows.Input;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.MasterDetail
{
    [Preserve(AllMembers = true)]
    public class ObjectTypeMenuItemViewModel : IMenuItem
    {
        public ObjectTypeMenuItemViewModel(
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
