using System;
using Plugin.Settings.Abstractions;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Settings
{
    /// <summary>
    /// Based off <see cref="https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/configuration-management"/>
    /// </summary>
    [Preserve(AllMembers = true)]
    public class AppSettings : IAppSettings
    {
        private readonly ISettings settings;

        public AppSettings(ISettings settings)
        {
            this.settings = settings;

            this.AzureHelpLink = new Uri("https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-group-create-service-principal-portal");
        }

        public Uri AzureHelpLink { get; }
    }
}
