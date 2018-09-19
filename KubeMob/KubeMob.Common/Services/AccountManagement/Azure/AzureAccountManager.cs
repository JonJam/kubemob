using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using KubeMob.Common.Exceptions;
using KubeMob.Common.Resx;
using KubeMob.Common.Services.AccountManagement.Azure.Model;
using KubeMob.Common.Services.AccountManagement.Model;
using KubeMob.Common.Services.Launcher;
using KubeMob.Common.Services.Settings;
using Microsoft.Azure.Management.ContainerService.Fluent;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.AccountManagement.Azure
{
    // <summary>
    // Had to install Microsoft.IdentityModel.Clients.ActiveDirectory Nuget directly in iOS, as without it a TypeInitializationException is thrown.
    //
    // Microsoft.Azure.Management includes built in retry logic.
    //
    // Configured linker to skip the following otherwise causes methods in this class to fail:
    // - Microsoft.Azure.Management.Fluent
    // - Microsoft.Azure.Management.ResourceManager.Fluent
    // - Microsoft.Azure.Management.ContainerService.Fluent
    // - Microsoft.Rest.ClientRuntime.Azure
    // - Microsoft.Rest.ClientRuntime.Azure.Authentication
    // - Microsoft.IdentityModel.Clients.ActiveDirectory
    // - Microsoft.Rest.ClientRuntime
    // - Newtonsoft.Json
    // </summary>
    public class AzureAccountManager : IAzureAccountManager
    {
        private const string AdalInvalidClientIdServiceErrorCode = "7001";
        private const string AdalInvalidClientSecretServiceErrorCode = "70002";
        private const string AdalTenantDoesntExistServiceErrorCode = "90002";
        private const int AdalRequestTimeoutStatusCode = 408;

        private static readonly Uri AzureHelpLink = new Uri(
            "https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-group-create-service-principal-portal");

        private readonly IAppSettings appSettings;
        private readonly ILauncher launcher;

        [Preserve]
        public AzureAccountManager(
            IAppSettings appSettings,
            ILauncher launcher)
        {
            this.appSettings = appSettings;
            this.launcher = launcher;

            this.Environments = new List<CloudEnvironment>()
            {
                new CloudEnvironment(
                    AzureEnvironment.AzureChinaCloud.AuthenticationEndpoint,
                    AppResources.AzureEnvironment_China),

                new CloudEnvironment(
                    AzureEnvironment.AzureGermanCloud.AuthenticationEndpoint,
                    AppResources.AzureEnvironment_German),

                new CloudEnvironment(
                    AzureEnvironment.AzureGlobalCloud.AuthenticationEndpoint,
                    AppResources.AzureEnvironment_Global,
                    true),

                new CloudEnvironment(
                    AzureEnvironment.AzureUSGovernment.AuthenticationEndpoint,
                    AppResources.AzureEnvironment_USGovernment)
            };
        }

        public CloudAccountType Key { get; } = CloudAccountType.Azure;

        public IList<CloudEnvironment> Environments
        {
            get;
        }

        public void LaunchHelp() => this.launcher.LaunchBrowser(AzureAccountManager.AzureHelpLink);

        public void SetSelectedCluster(Cluster cluster) => this.appSettings.SelectedCluster = cluster;

        public void RemoveSelectedCluster() => this.appSettings.SelectedCluster = null;

        public async Task<(bool isValid, string message)> TrySaveCredentials(
            CloudEnvironment cloudEnvironment,
            string tenantId,
            string clientId,
            string clientSecret,
            bool isEditing)
        {
            IEnumerable<AzureAccount> accounts = await this.appSettings
                .GetCloudAccounts<AzureAccount>(CloudAccountType.Azure);

            if (!isEditing &&
                accounts.Any(a => a.TenantId == tenantId))
            {
                // Ensuring not adding duplicate accounts, based upon TenantId.
                return (false, AppResources.AzureAccountManager_TryAddCredentials_DuplicateTenantId);
            }

            try
            {
                IAzure azure = AzureAccountManager.CreateAuthenticatedClient(cloudEnvironment.Id, tenantId, clientId, clientSecret);

                string subscriptionName = azure.GetCurrentSubscription().DisplayName;

                await this.appSettings.AddOrUpdateCloudAccount(new AzureAccount(
                    subscriptionName,
                    cloudEnvironment.Id,
                    tenantId,
                    clientId,
                    clientSecret));
            }
            catch (AdalServiceException e) when (e.ServiceErrorCodes != null &&
                e.ServiceErrorCodes.Contains(AzureAccountManager.AdalInvalidClientIdServiceErrorCode))
            {
                // AADSTS70001 - Invalid client ID.
                return (false, AppResources.AzureAccountManager_TryAddCredentials_InvalidClientId);
            }
            catch (AdalServiceException e) when (e.ServiceErrorCodes != null &&
                e.ServiceErrorCodes.Contains(AzureAccountManager.AdalInvalidClientSecretServiceErrorCode))
            {
                // AADSTS70002 - Invalid client secret.
                return (false, AppResources.AzureAccountManager_TryAddCredentials_InvalidClientSecret);
            }
            catch (AdalServiceException e) when (e.ServiceErrorCodes != null &&
                e.ServiceErrorCodes.Contains(AzureAccountManager.AdalTenantDoesntExistServiceErrorCode))
            {
                // AADSTS90002 - Tenant doesn't exist.
                return (false, AppResources.AzureAccountManager_TryAddCredentials_InvalidTenantId);
            }
            catch (AdalServiceException e) when (e.StatusCode == AzureAccountManager.AdalRequestTimeoutStatusCode)
            {
                // No internet
                return (false, AppResources.AzureAccountManager_TryAddCredentials_NoInternet);
            }
            catch (HttpRequestException e) when (e.InnerException is WebException web &&
                                                web.Status == WebExceptionStatus.NameResolutionFailure)
            {
                // No internet
                return (false, AppResources.AzureAccountManager_TryAddCredentials_NoInternet);
            }

            return (true, string.Empty);
        }

        public async Task<AzureAccount> GetAccount(string id)
        {
            IEnumerable<AzureAccount> azureAccounts = await this.appSettings
                .GetCloudAccounts<AzureAccount>(CloudAccountType.Azure);

            return azureAccounts.First((a) => a.TenantId == id);
        }

        public async Task<IEnumerable<ClusterGroup>> GetClusters()
        {
            List<ClusterGroup> groups = new List<ClusterGroup>();

            foreach (AzureAccount account in await this.appSettings
                .GetCloudAccounts<AzureAccount>(CloudAccountType.Azure))
            {
                ClusterGroup group = new ClusterGroup(
                    account.TenantId,
                    CloudAccountType.Azure,
                    account.Name);

                try
                {
                    IAzure azure = AzureAccountManager.CreateAuthenticatedClient(
                        account.EnvironmentId,
                        account.TenantId,
                        account.ClientId,
                        account.ClientSecret);

                    // TODO Handle paging ??
                    IPagedCollection<IKubernetesCluster> clusters = await azure.KubernetesClusters.ListAsync();

                    group.AddRange(clusters.Select(c => new Cluster(c.Id, c.Name, account.TenantId, CloudAccountType.Azure)));
                }
                catch (AdalServiceException e) when (e.ServiceErrorCodes != null &&
                                                    (e.ServiceErrorCodes.Contains(AzureAccountManager.AdalInvalidClientIdServiceErrorCode) ||
                                                    e.ServiceErrorCodes.Contains(AzureAccountManager.AdalInvalidClientSecretServiceErrorCode) ||
                                                    e.ServiceErrorCodes.Contains(AzureAccountManager.AdalTenantDoesntExistServiceErrorCode)))
                {
                    // Authentication issue
                    group.ErrorMessage = AppResources.AzureAccountManager_GetClusters_AuthenticationErrorMessage;
                }
                catch (AdalServiceException e) when (e.StatusCode == AzureAccountManager.AdalRequestTimeoutStatusCode)
                {
                    // No internet
                    throw new NoNetworkException(e.Message, e);
                }
                catch (HttpRequestException e) when (e.InnerException is WebException web &&
                                                     web.Status == WebExceptionStatus.NameResolutionFailure)
                {
                    // No internet
                    throw new NoNetworkException(e.Message, e);
                }

                groups.Add(group);
            }

            return groups;
        }

        public Task RemoveAccount(string id) => this.appSettings.RemoveCloudAccount(id);

        public async Task<byte[]> GetSelectedClusterKubeConfigContent()
        {
            Cluster selectedCluster = this.appSettings.SelectedCluster;
            IEnumerable<AzureAccount> accounts = await this.appSettings.GetCloudAccounts<AzureAccount>(CloudAccountType.Azure);

            AzureAccount account = accounts.First(a => a.Id == selectedCluster.AccountId);

            try
            {
                IAzure azure = AzureAccountManager.CreateAuthenticatedClient(
                    account.EnvironmentId,
                    account.TenantId,
                    account.ClientId,
                    account.ClientSecret);

                IKubernetesCluster kubernetesCluster = await azure.KubernetesClusters.GetByIdAsync(selectedCluster.Id);

                if (kubernetesCluster == null)
                {
                    // Cluster not found - mostly likely deleted.
                    throw new ClusterNotFoundException($"Cluster with Id: {selectedCluster.Id}");
                }

                return kubernetesCluster.UserKubeConfigContent;
            }
            catch (AdalServiceException e) when (e.ServiceErrorCodes != null &&
                                                 (e.ServiceErrorCodes.Contains(AzureAccountManager.AdalInvalidClientIdServiceErrorCode) ||
                                                 e.ServiceErrorCodes.Contains(AzureAccountManager.AdalInvalidClientSecretServiceErrorCode) ||
                                                 e.ServiceErrorCodes.Contains(AzureAccountManager.AdalTenantDoesntExistServiceErrorCode)))
            {
                // Something is wrong with the Account's credentials.
                throw new AccountInvalidException(e.Message, e);
            }
            catch (AdalServiceException e) when (e.StatusCode == AzureAccountManager.AdalRequestTimeoutStatusCode)
            {
                // No internet
                throw new NoNetworkException(e.Message, e);
            }
            catch (HttpRequestException e) when (e.InnerException is WebException web &&
                                                 web.Status == WebExceptionStatus.NameResolutionFailure)
            {
                // No internet
                throw new NoNetworkException(e.Message, e);
            }
        }

        private static IAzure CreateAuthenticatedClient(
            string environmentId,
            string tenantId,
            string clientId,
            string clientSecret)
        {
            AzureEnvironment environment =
                AzureEnvironment.KnownEnvironments.First(a => a.AuthenticationEndpoint == environmentId);

            AzureCredentials credentials = SdkContext.AzureCredentialsFactory.FromServicePrincipal(
                clientId,
                clientSecret,
                tenantId,
                environment);

            // TODO Add support for specifying subscription ID ??
            // TODO Check permissions to AKS ??
            return Microsoft.Azure.Management.Fluent.Azure.Authenticate(credentials).WithDefaultSubscription();
        }
    }
}
