using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using KubeMob.Common.Services.AccountManagement;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels
{
    [Preserve(AllMembers = true)]
    public class AddAzureAccountViewModel : ViewModelBase
    {
        private readonly IAccountManager accountManager;

        private CloudEnvironment selectedEnvironment;

        public AddAzureAccountViewModel(IAccountManager accountManager)
        {
            this.accountManager = accountManager;

            this.Environments = accountManager.Environments;
            this.SelectedEnvironment = accountManager.Environments.FirstOrDefault(e => e.IsDefault);

            this.ViewInformationCommand = new Command(() => this.accountManager.LaunchHelp());
        }

        public ICommand ViewInformationCommand { get; }

        public IList<CloudEnvironment> Environments { get; }

        public CloudEnvironment SelectedEnvironment
        {
            get => this.selectedEnvironment;
            set => this.SetProperty(ref this.selectedEnvironment, value);
        }
    }
}