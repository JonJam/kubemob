using System;
using System.Threading.Tasks;
using KubeMob.Common.Exceptions;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.Base
{
    [Preserve(AllMembers = true)]
    public abstract class ViewModelBase : ExtendedBindableObject
    {
        private bool isBusy;
        private bool hasNoNetwork;

        public bool IsBusy
        {
            get => this.isBusy;
            protected set => this.SetProperty(ref this.isBusy, value);
        }

        public bool HasNoNetwork
        {
            get => this.hasNoNetwork;
            private set => this.SetProperty(ref this.hasNoNetwork, value);
        }

        public virtual Task Initialize(object navigationData) => Task.CompletedTask;

        protected async Task PerformNetworkOperation(Func<Task> busyOperation)
        {
            this.IsBusy = true;
            this.HasNoNetwork = false;

            // Adding delay to give time for progress indicator to be displayed.
            await Task.Delay(100);

            try
            {
                await busyOperation();
            }
            catch (NoNetworkException)
            {
                this.HasNoNetwork = true;
            }
            finally
            {
                this.IsBusy = false;
            }
        }
    }
}
