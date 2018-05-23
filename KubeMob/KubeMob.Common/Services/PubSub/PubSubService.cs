using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.PubSub
{
    public class PubSubService : IPubSubService
    {
        private const string ResourceListingSettingChangeMessage = "ResourceListingSettingChanged";

        [Preserve]
        public PubSubService()
        {
        }

        public void SubscribeToResourceListingSettingChanged<T>(
            T obj,
            Action<T, string> ca)
            where T : class => MessagingCenter.Subscribe(obj, PubSubService.ResourceListingSettingChangeMessage, ca);

        public void PublishResourceListingSettingChanged<T>(
            T obj,
            string resourceName)
            where T : class => MessagingCenter.Send(
                obj,
                PubSubService.ResourceListingSettingChangeMessage,
                resourceName);
    }
}
