using System.Threading.Tasks;

namespace KubeMob.Common.ViewModels.Base
{
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
