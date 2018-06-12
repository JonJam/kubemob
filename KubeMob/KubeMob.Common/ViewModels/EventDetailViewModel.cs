using System.Threading.Tasks;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels
{
    [Preserve(AllMembers = true)]
    public class EventDetailViewModel : ViewModelBase
    {
        public EventDetailViewModel()
        {
        }

        public override Task Initialize(object navigationData)
        {
            Event eventDetail = (Event)navigationData;

            return Task.CompletedTask;
        }
    }
}
