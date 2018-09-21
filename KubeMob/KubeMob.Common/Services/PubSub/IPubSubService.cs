using System;

namespace KubeMob.Common.Services.PubSub
{
    public interface IPubSubService
    {
        void SubscribeToResourceListingSettingChanged<TSender>(
            object sender,
            Action<TSender, string> ca)
            where TSender : class;

        void SubscribeToNamespaceChanged<TSender>(
           object sender,
           Action<TSender> ca)
            where TSender : class;

        void PublishResourceListingSettingChanged<TSender>(
            TSender sender,
            string resourceName)
            where TSender : class;

        void PublishNamespaceChanged<TSender>(
           TSender sender)
           where TSender : class;
    }
}
