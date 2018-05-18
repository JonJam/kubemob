using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KubeMob.Common.Services.AccountManagement.Model;
using Newtonsoft.Json;
using Plugin.Settings.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Settings
{
    /// <summary>
    /// Based off <see cref="https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/configuration-management"/>
    /// 
    /// In order to Xamarin.Auth for secure storage had to install Xamarin.Android.Support.CustomTabs directly.
    /// </summary>
    public class AppSettings : IAppSettings
    {
        private const string CloudAccountsKey = "KubeMob-CloudAccounts";

        private readonly ISettings settings;

        [Preserve]
        public AppSettings(
            ISettings settings)
        {
            this.settings = settings;

            this.AzureHelpLink =
                new Uri(
                    "https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-group-create-service-principal-portal");
        }

        public Uri AzureHelpLink
        {
            get;
        }

        public Cluster SelectedCluster
        {
            get
            {
                string settingValue = this.settings.GetValueOrDefault(nameof(this.SelectedCluster), null);

                return settingValue != null
                    ? JsonConvert.DeserializeObject<Cluster>(settingValue)
                    : null;
            }
            set => this.settings.AddOrUpdateValue(
                nameof(this.SelectedCluster),
                JsonConvert.SerializeObject(value));
        }

        public async Task<IEnumerable<T>> GetCloudAccounts<T>(
            CloudAccountType accountType)
            where T : CloudAccount
        {
            string savedAccountsRaw = await SecureStorage.GetAsync(AppSettings.CloudAccountsKey);

            if (string.IsNullOrWhiteSpace(savedAccountsRaw))
            {
                return new List<T>();
            }

            List<T> accounts = JsonConvert.DeserializeObject<List<T>>(savedAccountsRaw);

            // Having to create a new list after deserialization, as for some reason performing
            // where causes the app to crash.
            accounts = new List<T>(accounts);

            return accounts.Where(ca => ca.AccountType == accountType);
        }

        public async Task AddOrUpdateCloudAccount(CloudAccount cloudAccount)
        {
            string savedAccountsRaw = await SecureStorage.GetAsync(AppSettings.CloudAccountsKey);

            List<CloudAccount> accountsToSave = new List<CloudAccount>();

            if (!string.IsNullOrWhiteSpace(savedAccountsRaw))
            {
                accountsToSave = JsonConvert.DeserializeObject<List<CloudAccount>>(savedAccountsRaw);
            }

            accountsToSave.Add(cloudAccount);

            await SecureStorage.SetAsync(AppSettings.CloudAccountsKey, JsonConvert.SerializeObject(accountsToSave));
        }

        public async Task RemoveCloudAccount(string id)
        {
            string savedAccountRaw = await SecureStorage.GetAsync(AppSettings.CloudAccountsKey);

            if (!string.IsNullOrWhiteSpace(savedAccountRaw))
            {
                List<CloudAccount> savedAccounts = JsonConvert.DeserializeObject<List<CloudAccount>>(savedAccountRaw);

                // Removing account.
                savedAccounts = savedAccounts.Where(c => c.Id != id).ToList();

                await SecureStorage.SetAsync(AppSettings.CloudAccountsKey, JsonConvert.SerializeObject(savedAccounts));
            }
        }
    }
}