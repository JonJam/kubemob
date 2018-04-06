using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using KubeMob.Common.Resx;
using KubeMob.Common.Services.AccountManagement;
using KubeMob.Common.Validation;
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

            this.TenantId = new ValidatableObject<string>(
                new List<IValidationRule<string>>()
                {
                    new IsNotNullOrEmptyRule<string>
                    {
                        ValidationMessage = AppResources.AddAzureAccountPage_TenantId_IsNotNullOrEmptyRule_Message
                    }
                });
        }

        public ICommand ViewInformationCommand => new Command(() => this.accountManager.LaunchHelp());

        public ICommand ValidateTenantIdCommand => new Command(() => this.TenantId.Validate());

        public IList<CloudEnvironment> Environments { get; }

        public CloudEnvironment SelectedEnvironment
        {
            get => this.selectedEnvironment;
            set => this.SetProperty(ref this.selectedEnvironment, value);
        }

        public ValidatableObject<string> TenantId { get; }

        // TODO Wire up calling this from a save button
        private bool Validate()
        {
            return this.TenantId.Validate();
        }
    }
}