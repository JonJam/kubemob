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

        public void SubscribeToResourceListingSettingChanged<TSender>(
            object sender,
            Action<TSender, string> ca)
            where TSender : class => MessagingCenter.Subscribe(sender, PubSubService.ResourceListingSettingChangeMessage, ca);

        public void PublishResourceListingSettingChanged<TSender>(
            TSender sender,
            string resourceName)
            where TSender : class => MessagingCenter.Send(
                sender,
                PubSubService.ResourceListingSettingChangeMessage,
                resourceName);
    }
}
