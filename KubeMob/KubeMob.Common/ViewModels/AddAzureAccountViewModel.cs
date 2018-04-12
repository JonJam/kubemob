using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using KubeMob.Common.Resx;
using KubeMob.Common.Services.AccountManagement;
using KubeMob.Common.Services.AccountManagement.Azure;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Validation;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels
{
    [Preserve(AllMembers = true)]
    public class AddAzureAccountViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private readonly IAzureAccountManager azureAccountManager;

        private string topLevelErrorMessage;

        private CloudEnvironment selectedEnvironment;

        public AddAzureAccountViewModel(
            INavigationService navigationService,
            IAzureAccountManager azureAccountManager)
        {
            this.navigationService = navigationService;
            this.azureAccountManager = azureAccountManager;

            this.Environments = azureAccountManager.Environments;
            this.SelectedEnvironment = azureAccountManager.Environments.FirstOrDefault(e => e.IsDefault);

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

        public ICommand ViewInformationCommand => new Command(() => this.azureAccountManager.LaunchHelp());

        public ICommand ValidateTenantIdCommand => new Command(() => this.TenantId.Validate());

        public ICommand ValidateClientIdCommand => new Command(() => this.ClientId.Validate());

        public ICommand ValidateClientSecretCommand => new Command(() => this.ClientSecret.Validate());

        public ICommand AddCommand => new Command(this.AddAccount);

        public string TopLevelErrorMessage
        {
            get => this.topLevelErrorMessage;
            private set
            {
                if (this.SetProperty(ref this.topLevelErrorMessage, value))
                {
                    this.NotifyPropertyChanged(() => this.HasTopLevelErrorMessage);
                }
            }
        }

        public bool HasTopLevelErrorMessage => !string.IsNullOrWhiteSpace(this.topLevelErrorMessage);

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
            this.TopLevelErrorMessage = null;

            CloudEnvironment env = this.SelectedEnvironment;
            string tenant = this.TenantId.Value;
            string client = this.ClientId.Value;
            string secret = this.ClientSecret.Value;

            if (!this.Validate())
            {
                return;
            }

            this.IsBusy = true;

            (bool isValid, string message) = this.azureAccountManager.TryAddCredentials(
                env,
                tenant,
                client,
                secret);

            if (isValid)
            {
                this.navigationService.GoBack(2);
            }
            else
            {
                this.TopLevelErrorMessage = message;
            }

            this.IsBusy = false;
        }

        private bool Validate() => this.ClientId.Validate() &&
                                   this.ClientSecret.Validate() &&
                                   this.TenantId.Validate();
    }
}