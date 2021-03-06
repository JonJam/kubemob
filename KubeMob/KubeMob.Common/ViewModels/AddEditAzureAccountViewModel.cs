using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using KubeMob.Common.Resx;
using KubeMob.Common.Services.AccountManagement.Azure;
using KubeMob.Common.Services.AccountManagement.Azure.Model;
using KubeMob.Common.Services.AccountManagement.Model;
using KubeMob.Common.Services.Navigation;
using KubeMob.Common.Validation;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels
{
    [Preserve(AllMembers = true)]
    public class AddEditAzureAccountViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private readonly IAzureAccountManager azureAccountManager;

        private string topLevelErrorMessage;
        private bool isEditing;

        private CloudEnvironment selectedEnvironment;

        public AddEditAzureAccountViewModel(
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
                        ValidationMessage = AppResources.AddEditAzureAccountPage_TenantId_IsNotNullOrEmptyRule_Message
                    }
                });

            this.ClientId = new ValidatableObject<string>(
                new List<IValidationRule<string>>()
                {
                    new IsNotNullOrEmptyRule<string>
                    {
                        ValidationMessage = AppResources.AddEditAzureAccountPage_ClientId_IsNotNullOrEmptyRule_Message
                    }
                });

            this.ClientSecret = new ValidatableObject<string>(
                new List<IValidationRule<string>>()
                {
                    new IsNotNullOrEmptyRule<string>
                    {
                        ValidationMessage =
                            AppResources.AddEditAzureAccountPage_ClientSecret_IsNotNullOrEmptyRule_Message
                    }
                });
        }

        public ICommand ViewInformationCommand => new Command(() => this.azureAccountManager.LaunchHelp());

        public ICommand ValidateTenantIdCommand => new Command(() => this.TenantId.Validate());

        public ICommand ValidateClientIdCommand => new Command(() => this.ClientId.Validate());

        public ICommand ValidateClientSecretCommand => new Command(() => this.ClientSecret.Validate());

        public ICommand SaveCommand => new Command(async () => await this.SaveAccount());

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

        public bool IsEditing
        {
            get => this.isEditing;
            private set => this.SetProperty(ref this.isEditing, value);
        }

        public IList<CloudEnvironment> Environments
        {
            get;
        }

        public CloudEnvironment SelectedEnvironment
        {
            get => this.selectedEnvironment;
            set => this.SetProperty(ref this.selectedEnvironment, value);
        }

        public ValidatableObject<string> TenantId
        {
            get;
        }

        public ValidatableObject<string> ClientId
        {
            get;
        }

        public ValidatableObject<string> ClientSecret
        {
            get;
        }

        public override async Task Initialize(object navigationData)
        {
            if (navigationData is string accountId)
            {
                // Existing account to edit, populate fields and make tenant read-only.
                this.IsEditing = true;

                AzureAccount azureAccount = await this.azureAccountManager.GetAccount(accountId);

                this.SelectedEnvironment = this.Environments.First(e => e.Id == azureAccount.EnvironmentId);
                this.TenantId.Value = azureAccount.TenantId;
                this.ClientId.Value = azureAccount.ClientId;
                this.ClientSecret.Value = azureAccount.ClientSecret;
            }
        }

        private Task SaveAccount() => this.PerformNetworkOperation(async () =>
        {
            this.TopLevelErrorMessage = null;

            if (this.Validate())
            {
                CloudEnvironment env = this.SelectedEnvironment;
                string tenant = this.TenantId.Value;
                string client = this.ClientId.Value;
                string secret = this.ClientSecret.Value;

                (bool isValid, string message) = await this.azureAccountManager.TrySaveCredentials(
                    env,
                    tenant,
                    client,
                    secret,
                    this.IsEditing);

                if (isValid)
                {
                    await this.navigationService.GoBackToClustersPage();
                }
                else
                {
                    this.TopLevelErrorMessage = message;
                }
            }
        });

        private bool Validate()
        {
            // Writing like this to ensure validation done for all properties in order
            // to display validation messages if need to. If combined these into statement,
            // then if first was false than the remaining wouldn't be evaluated.
            bool clientIdValid = this.ClientId.Validate();
            bool clientSecretValid = this.ClientSecret.Validate();
            bool tenantIdValid = this.TenantId.Validate();

            return clientIdValid && clientSecretValid && tenantIdValid;
        }
    }
}