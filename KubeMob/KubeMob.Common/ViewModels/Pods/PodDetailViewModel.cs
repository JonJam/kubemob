using System.Threading.Tasks;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.Pods
{
    [Preserve(AllMembers = true)]
    public class PodDetailViewModel : ViewModelBase
    {
        public PodDetailViewModel()
        {
        }

        public override Task Initialize(object navigationData)
        {
            ObjectId objectId = (ObjectId)navigationData;

            return base.Initialize(navigationData);
        }
    }
}
