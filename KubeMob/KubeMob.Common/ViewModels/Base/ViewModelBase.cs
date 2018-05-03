using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.Base
{
    [Preserve(AllMembers = true)]
    public abstract class ViewModelBase : ExtendedBindableObject
    {
        private bool isBusy;

        public bool IsBusy
        {
            get => this.isBusy;
            set => this.SetProperty(ref this.isBusy, value);
        }

        public virtual Task Initialize(object navigationData) => Task.CompletedTask;
    }
}
