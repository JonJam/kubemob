using System;
using System.Collections.Generic;
using KubeMob.Common.Services.AccountManagement.Azure;
using Newtonsoft.Json;
using Plugin.Settings.Abstractions;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Settings
{
    /// <summary>
    /// Based off <see cref="https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/configuration-management"/>
    /// </summary>
    public class AppSettings : IAppSettings
    {
        private const string AzureAccountsKey = "AzureAccounts";

        private readonly ISettings settings;

        [Preserve]
        public AppSettings(ISettings settings)
        {
            this.settings = settings;

            this.AzureHelpLink =
                new Uri(
                    "https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-group-create-service-principal-portal");
        }

        public Uri AzureHelpLink { get; }

        public List<AzureAccount> GetAzureAccounts()
        {
            string settingValue = this.settings.GetValueOrDefault(AppSettings.AzureAccountsKey, null);

            return settingValue != null
                ? JsonConvert.DeserializeObject<List<AzureAccount>>(settingValue)
                : new List<AzureAccount>();
        }

        public void SetAzureAccounts(List<AzureAccount> accounts) =>
            this.settings.AddOrUpdateValue(AppSettings.AzureAccountsKey, JsonConvert.SerializeObject(accounts));
    }
}