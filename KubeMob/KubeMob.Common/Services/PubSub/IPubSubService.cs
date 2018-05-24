using System;

namespace KubeMob.Common.Services.PubSub
{
    public interface IPubSubService
    {
        void SubscribeToResourceListingSettingChanged<TSender>(
            object sender,
            Action<TSender, string> ca)
            where TSender : class;

        void PublishResourceListingSettingChanged<TSender>(
            TSender sender,
            string resourceName)
            where TSender : class;
    }
}
