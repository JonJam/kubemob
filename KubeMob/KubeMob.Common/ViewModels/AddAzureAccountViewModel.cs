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

            this.ClientId = new ValidatableObject<string>(
                new List<IValidationRule<string>>()
                {
                    new IsNotNullOrEmptyRule<string>
                    {
                        ValidationMessage = AppResources.AddAzureAccountPage_ClientId_IsNotNullOrEmptyRule_Message
                    }
                });

            this.ClientSecret = new ValidatableObject<string>(
                new List<IValidationRule<string>>()
                {
                    new IsNotNullOrEmptyRule<string>
                    {
                        ValidationMessage = AppResources.AddAzureAccountPage_ClientSecret_IsNotNullOrEmptyRule_Message
                    }
                });
        }

        public ICommand ViewInformationCommand => new Command(() => this.accountManager.LaunchHelp());

        public ICommand ValidateTenantIdCommand => new Command(() => this.TenantId.Validate());

        public ICommand ValidateClientIdCommand => new Command(() => this.ClientId.Validate());

        public ICommand ValidateClientSecretCommand => new Command(() => this.ClientSecret.Validate());

        public ICommand AddCommand => new Command(this.AddAccount);

        public IList<CloudEnvironment> Environments { get; }

        public CloudEnvironment SelectedEnvironment
        {
            get => this.selectedEnvironment;
            set => this.SetProperty(ref this.selectedEnvironment, value);
        }

        public ValidatableObject<string> TenantId { get; }

        public ValidatableObject<string> ClientId { get; }

        public ValidatableObject<string> ClientSecret { get; }

        private void AddAccount()
        {
            if (this.Validate())
            {
                // TODO loading
                // TODO attempt to login into azure
                // - if fail, display overall error,
                // - if success, save creds and navigate
            }
        }

        private bool Validate()
        {
            return this.ClientId.Validate() &&
                   this.ClientSecret.Validate() &&
                   this.TenantId.Validate();
        }
    }
}