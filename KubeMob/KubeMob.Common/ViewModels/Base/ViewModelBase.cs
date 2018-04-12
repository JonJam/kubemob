using System.Threading.Tasks;

namespace KubeMob.Common.ViewModels.Base
{
    public abstract class ViewModelBase : ExtendedBindableObject
    {
        private bool isBusy;

        public bool IsBusy
        {
            get => this.isBusy;
            set
            {
                if (this.SetProperty(ref this.isBusy, value))
                {
                    this.NotifyPropertyChanged(() => this.IsNotBusy);
                }
            }
        }

        public bool IsNotBusy => !this.IsBusy;

        public virtual Task Initialize(object navigationData) => Task.CompletedTask;
    }
}
