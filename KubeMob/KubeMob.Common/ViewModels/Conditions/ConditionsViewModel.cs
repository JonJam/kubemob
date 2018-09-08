using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Condition = KubeMob.Common.Services.Kubernetes.Model.Condition;

namespace KubeMob.Common.ViewModels.Conditions
{
    [Preserve(AllMembers = true)]
    public class ConditionsViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;

        private IList<Condition> conditions = new List<Condition>();

        public ConditionsViewModel(
            INavigationService navigationService)
        {
            this.navigationService = navigationService;

            this.ConditionSelectedCommand = new Command(
                async (o) => await this.OnConditionSelectedExecute(o));
        }

        public ICommand ConditionSelectedCommand
        {
            get;
        }

        public IList<Condition> Conditions
        {
            get => this.conditions;
            private set
            {
                if (this.SetProperty(ref this.conditions, value))
                {
                    this.NotifyPropertyChanged(() => this.HasConditions);
                }
            }
        }

        public bool HasConditions => this.Conditions.Count > 0;

        public override Task Initialize(object navigationData)
        {
            this.Conditions = (IList<Condition>)navigationData;

            return Task.CompletedTask;
        }

        private async Task OnConditionSelectedExecute(object obj) => await this.navigationService.NavigateToConditionDetailPage((Condition)obj);
    }
}
