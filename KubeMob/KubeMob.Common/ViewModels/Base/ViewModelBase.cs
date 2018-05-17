using System;
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
            private set => this.SetProperty(ref this.isBusy, value);
        }

        public virtual Task Initialize(object navigationData) => Task.CompletedTask;

        protected Task PerformBusyOperation<T>(Func<Task> busyOperation)
        {
            this.IsBusy = true;

            try
            {
                return busyOperation();
            }
            finally
            {
                this.IsBusy = false;
            }
        }

        protected Task<T> PerformBusyOperation<T>(Func<Task<T>> busyOperation)
        {
            this.IsBusy = true;

            try
            {
                return busyOperation();
            }
            finally
            {
                this.IsBusy = false;
            }
        }
    }
}
