using System.Threading.Tasks;
using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.Conditions
{
    [Preserve(AllMembers = true)]
    public class ConditionDetailViewModel : ViewModelBase
    {
        private Condition detail;

        public ConditionDetailViewModel() => this.IsBusy = true;

        public Condition Detail
        {
            get => this.detail;
            private set => this.SetProperty(ref this.detail, value);
        }

        public override Task Initialize(object navigationData)
        {
            this.Detail = (Condition)navigationData;

            this.IsBusy = false;

            return Task.CompletedTask;
        }
    }
}
