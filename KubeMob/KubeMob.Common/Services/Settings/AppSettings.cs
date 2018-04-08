using System;
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
        private readonly ISettings settings;

        [Preserve]
        public AppSettings(ISettings settings)
        {
            this.settings = settings;

            this.AzureHelpLink = new Uri("https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-group-create-service-principal-portal");
        }

        public Uri AzureHelpLink { get; }

        // TODO Support more than one account ??
        public AzureAccount AzureAccount
        {
            get => JsonConvert.DeserializeObject<AzureAccount>(this.settings.GetValueOrDefault(nameof(this.AzureAccount), null));
            set => this.settings.AddOrUpdateValue(nameof(this.AzureAccount), JsonConvert.SerializeObject(value));
        }
    }
}