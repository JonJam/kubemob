using System.Threading.Tasks;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels
{
    [Preserve(AllMembers = true)]
    public class EventDetailViewModel : ViewModelBase
    {
        private Event detail;

        public EventDetailViewModel()
        {
        }

        public Event Detail
        {
            get => this.detail;
            private set => this.SetProperty(ref this.detail, value);
        }

        public override Task Initialize(object navigationData)
        {
            this.Detail = (Event)navigationData;

            return Task.CompletedTask;
        }
    }
}
