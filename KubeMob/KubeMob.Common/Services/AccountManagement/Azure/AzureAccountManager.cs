﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using KubeMob.Common.Resx;
using KubeMob.Common.Services.Kubernetes;
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
    public class AzureAccountManager : IAzureAccountManager
    {
        private readonly IAppSettings appSettings;

        [Preserve]
        public AzureAccountManager(IAppSettings appSettings)
        {
            this.appSettings = appSettings;

            this.Environments = new List<CloudEnvironment>()
            {
                new CloudEnvironment(
                    AzureEnvironment.AzureChinaCloud.ToString(),
                    AppResources.AzureEnvironment_China),

                new CloudEnvironment(
                    AzureEnvironment.AzureGermanCloud.ToString(),
                    AppResources.AzureEnvironment_German),

                new CloudEnvironment(
                    AzureEnvironment.AzureGlobalCloud.ToString(),
                    AppResources.AzureEnvironment_Global,
                    true),

                new CloudEnvironment(
                    AzureEnvironment.AzureUSGovernment.ToString(),
                    AppResources.AzureEnvironment_USGovernment)
            };
        }

        public IList<CloudEnvironment> Environments { get; }

        public void LaunchHelp() => Device.OpenUri(this.appSettings.AzureHelpLink);

        public (bool isValid, string message) IsValidCredentials(
            CloudEnvironment cloudEnvironment,
            string tenantId,
            string clientId,
            string clientSecret)
        {
            try
            {
                AzureAccountManager.CreateAuthenticatedClient(cloudEnvironment.Id, tenantId, clientId, clientSecret);
            }
            catch (AdalServiceException e) when (e.ServiceErrorCodes.Contains("70001"))
            {
                // AADSTS70001 - Invalid client ID.
                return (false, AppResources.AzureAccountManager_IsValidCredentials_InvalidClientId);
            }
            catch (AdalServiceException e) when (e.ServiceErrorCodes.Contains("70002"))
            {
                // AADSTS70002 - Invalid client secret.
                return (false, AppResources.AzureAccountManager_IsValidCredentials_InvalidClientSecret);
            }
            catch (AdalServiceException e) when (e.ServiceErrorCodes.Contains("90002"))
            {
                // AADSTS90002 - Tenant doesn't exist.
                return (false, AppResources.AzureAccountManager_IsValidCredentials_InvalidTenantId);
            }
            catch (HttpRequestException e) when (e.InnerException is WebException web &&
                                                web.Status == WebExceptionStatus.NameResolutionFailure)
            {
                // No internet
                return (false, AppResources.AzureAccountManager_IsValidCredentials_NoInternet);
            }

            return (true, string.Empty);
        }


        public void AddCredentials(
            CloudEnvironment cloudEnvironment,
            string tenantId,
            string clientId,
            string clientSecret)
        {
            // TODO Support more than one account?? Need to validate don't add dupes.
            this.appSettings.AzureAccount = new AzureAccount()
            {
                ClientId = clientId,
                ClientSecret = clientSecret,
                EnvironmentId = cloudEnvironment.Id,
                TenantId = tenantId
            };
        }

        public async Task<IEnumerable<ClusterSummaryGroup>> GetClusters()
        {
            // TODO Support more than one account
            AzureAccount account = this.appSettings.AzureAccount;

            if (account == null)
            {
                return new List<ClusterSummaryGroup>();
            }

            // TODO Error handling
            IAzure azure = AzureAccountManager.CreateAuthenticatedClient(
                account.EnvironmentId,
                account.TenantId,
                account.ClientId,
                account.ClientSecret);

            string subscriptionName = azure.GetCurrentSubscription().DisplayName;

            // Includes built in retry logic. Configured linker to skip the following otherwise causes this to fail:
            // - Microsoft.Azure.Management.ContainerService.Fluent
            // TODO Handle paging ??
            IPagedCollection<IKubernetesCluster> clusters = await azure.KubernetesClusters.ListAsync();

            ClusterSummaryGroup group = new ClusterSummaryGroup(subscriptionName);

            // TODO add locaiton, resource group as secondary text ??
            group.AddRange(clusters.Select(Mapper.Map<ClusterSummary>));

            return new List<ClusterSummaryGroup>()
            {
                group
            };
        }

        //IKubernetesCluster kubernetesCluster = a.KubernetesClusters.GetByResourceGroup("tpb", aksId);
        //var b = kubernetesCluster.UserKubeConfigContent;
        ////var c = kubernetesCluster.AdminKubeConfigContent;

        //var config = new KubernetesClientConfiguration();

        //    using (Stream stream = new MemoryStream(b))
        //{
        //    config = KubernetesClientConfiguration.BuildConfigFromConfigFile(stream);
        //}

        //IKubernetes client = new Kubernetes(config);

        private static IAzure CreateAuthenticatedClient(
            string environmentId,
            string tenantId,
            string clientId,
            string clientSecret)
        {
            AzureEnvironment environment =
                AzureEnvironment.KnownEnvironments.First(a => a.ToString() == environmentId);

            AzureCredentials credentials = SdkContext.AzureCredentialsFactory.FromServicePrincipal(
                clientId,
                clientSecret,
                tenantId,
                environment);

            // TODO Add support for specifying subscription ID ??
            // TODO Check permissions to AKS ??
            // Includes built in retry logic. Configured linker to skip the following otherwise causes this to fail:
            // - Microsoft.Azure.Management.Fluent
            // - Microsoft.Azure.Management.ResourceManager.Fluent
            // - Microsoft.Rest.ClientRuntime.Azure
            // - Microsoft.Rest.ClientRuntime.Azure.Authentication
            // - Microsoft.IdentityModel.Clients.ActiveDirectory
            // - Microsoft.Rest.ClientRuntime
            // - Newtonsoft.Json
            return Microsoft.Azure.Management.Fluent.Azure.Authenticate(credentials).WithDefaultSubscription();
        }
    }
}