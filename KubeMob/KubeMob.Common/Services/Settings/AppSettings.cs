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
    /// To nuget warning caused by Xamarin.Essentials had to install Xamarin.Android.Support.CustomTabs directly.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class AppSettings : IAppSettings
    {
        private const string CloudAccountsKey = "KubeMob-CloudAccounts";

        private readonly ISettings settings;

        public AppSettings(
            ISettings settings) => this.settings = settings;

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

        public string SelectedNamespace
        {
            get => this.settings.GetValueOrDefault(nameof(this.SelectedNamespace), string.Empty);
            set => this.settings.AddOrUpdateValue(
                nameof(this.SelectedNamespace),
                value);
        }

        public bool ShowNamespaces
        {
            get => this.settings.GetValueOrDefault(nameof(this.ShowNamespaces), true);
            set => this.settings.AddOrUpdateValue(
                nameof(this.ShowNamespaces),
                value);
        }

        public bool ShowNodes
        {
            get => this.settings.GetValueOrDefault(nameof(this.ShowNodes), true);
            set => this.settings.AddOrUpdateValue(
                nameof(this.ShowNodes),
                value);
        }

        public bool ShowPersistentVolumes
        {
            get => this.settings.GetValueOrDefault(nameof(this.ShowPersistentVolumes), true);
            set => this.settings.AddOrUpdateValue(
                nameof(this.ShowPersistentVolumes),
                value);
        }

        public bool ShowStorageClasses
        {
            get => this.settings.GetValueOrDefault(nameof(this.ShowStorageClasses), true);
            set => this.settings.AddOrUpdateValue(
                nameof(this.ShowStorageClasses),
                value);
        }

        public bool ShowRoles
        {
            get => this.settings.GetValueOrDefault(nameof(this.ShowRoles), true);
            set => this.settings.AddOrUpdateValue(
                nameof(this.ShowRoles),
                value);
        }

        public bool ShowCronJobs
        {
            get => this.settings.GetValueOrDefault(nameof(this.ShowCronJobs), true);
            set => this.settings.AddOrUpdateValue(
                nameof(this.ShowCronJobs),
                value);
        }

        public bool ShowDaemonSets
        {
            get => this.settings.GetValueOrDefault(nameof(this.ShowDaemonSets), true);
            set => this.settings.AddOrUpdateValue(
                nameof(this.ShowDaemonSets),
                value);
        }

        public bool ShowDeployments
        {
            get => this.settings.GetValueOrDefault(nameof(this.ShowDeployments), true);
            set => this.settings.AddOrUpdateValue(
                nameof(this.ShowDeployments),
                value);
        }

        public bool ShowJobs
        {
            get => this.settings.GetValueOrDefault(nameof(this.ShowJobs), true);
            set => this.settings.AddOrUpdateValue(
                nameof(this.ShowJobs),
                value);
        }

        public bool ShowPods
        {
            get => this.settings.GetValueOrDefault(nameof(this.ShowPods), true);
            set => this.settings.AddOrUpdateValue(
                nameof(this.ShowPods),
                value);
        }

        public bool ShowReplicaSets
        {
            get => this.settings.GetValueOrDefault(nameof(this.ShowReplicaSets), true);
            set => this.settings.AddOrUpdateValue(
                nameof(this.ShowReplicaSets),
                value);
        }

        public bool ShowReplicationControllers
        {
            get => this.settings.GetValueOrDefault(nameof(this.ShowReplicationControllers), true);
            set => this.settings.AddOrUpdateValue(
                nameof(this.ShowReplicationControllers),
                value);
        }

        public bool ShowStatefulSets
        {
            get => this.settings.GetValueOrDefault(nameof(this.ShowStatefulSets), true);
            set => this.settings.AddOrUpdateValue(
                nameof(this.ShowStatefulSets),
                value);
        }

        public bool ShowIngresses
        {
            get => this.settings.GetValueOrDefault(nameof(this.ShowIngresses), true);
            set => this.settings.AddOrUpdateValue(
                nameof(this.ShowIngresses),
                value);
        }

        public bool ShowServices
        {
            get => this.settings.GetValueOrDefault(nameof(this.ShowServices), true);
            set => this.settings.AddOrUpdateValue(
                nameof(this.ShowServices),
                value);
        }

        public bool ShowConfigMaps
        {
            get => this.settings.GetValueOrDefault(nameof(this.ShowConfigMaps), true);
            set => this.settings.AddOrUpdateValue(
                nameof(this.ShowConfigMaps),
                value);
        }

        public bool ShowPersistentVolumeClaims
        {
            get => this.settings.GetValueOrDefault(nameof(this.ShowPersistentVolumeClaims), true);
            set => this.settings.AddOrUpdateValue(
                nameof(this.ShowPersistentVolumeClaims),
                value);
        }

        public bool ShowSecrets
        {
            get => this.settings.GetValueOrDefault(nameof(this.ShowSecrets), true);
            set => this.settings.AddOrUpdateValue(
                nameof(this.ShowSecrets),
                value);
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