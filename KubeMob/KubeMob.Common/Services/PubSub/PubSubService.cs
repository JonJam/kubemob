using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.PubSub
{
    public class PubSubService : IPubSubService
    {
        private const string ResourceListingSettingChangeMessage = "ResourceListingSettingChanged";
        private const string NamespaceChangeMessage = "NamespaceChanged";

        [Preserve]
        public PubSubService()
        {
        }

        public void SubscribeToResourceListingSettingChanged<TSender>(
            object sender,
            Action<TSender, string> ca)
            where TSender : class => MessagingCenter.Subscribe(sender, PubSubService.ResourceListingSettingChangeMessage, ca);

        public void SubscribeToNamespaceChanged<TSender>(
            object sender,
            Action<TSender> ca)
            where TSender : class => MessagingCenter.Subscribe(sender, PubSubService.NamespaceChangeMessage, ca);

        public void PublishResourceListingSettingChanged<TSender>(
            TSender sender,
            string resourceName)
            where TSender : class => MessagingCenter.Send(
                sender,
                PubSubService.ResourceListingSettingChangeMessage,
                resourceName);

        public void PublishNamespaceChanged<TSender>(
            TSender sender)
            where TSender : class => MessagingCenter.Send(
            sender,
            PubSubService.NamespaceChangeMessage);
    }
}
