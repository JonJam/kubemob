using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KubeMob.Common.Services.AccountManagement;
using Newtonsoft.Json;
using Plugin.Settings.Abstractions;
using Xamarin.Auth;
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
        private readonly AccountStore accountStore;

        [Preserve]
        public AppSettings(
            ISettings settings,
            AccountStore accountStore)
        {
            this.settings = settings;
            this.accountStore = accountStore;

            this.AzureHelpLink =
                new Uri(
                    "https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-group-create-service-principal-portal");
        }

        public Uri AzureHelpLink { get; }

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
            List<Account> accounts = await this.accountStore
                .FindAccountsForServiceAsync(AppSettings.CloudAccountsKey);

            List<T> cloudAccounts = Mapper.Map<List<T>>(accounts);

            return cloudAccounts.Where(ca => ca.AccountType == accountType);
        }

        public Task AddOrUpdateCloudAccount(CloudAccount cloudAccount)
        {
            Account account = Mapper.Map<Account>(cloudAccount);

            return this.accountStore.SaveAsync(account, AppSettings.CloudAccountsKey);
        }

        public Task RemoveCloudAccount(string id)
        {
            Account account = this.accountStore
                .FindAccountsForService(AppSettings.CloudAccountsKey)
                .FirstOrDefault(a => a.Username == id);

            return account != null ?
                this.accountStore.DeleteAsync(account, AppSettings.CloudAccountsKey) : 
                Task.CompletedTask;
        }
    }
}