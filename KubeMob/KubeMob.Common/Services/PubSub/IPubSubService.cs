using System;

namespace KubeMob.Common.Services.PubSub
{
    public interface IPubSubService
    {
        void SubscribeToResourceListingSettingChanged<T>(
            T obj,
            Action<T, string> ca)
            where T : class;

        void PublishResourceListingSettingChanged<T>(
            T obj,
            string resourceName)
            where T : class;
    }
}
