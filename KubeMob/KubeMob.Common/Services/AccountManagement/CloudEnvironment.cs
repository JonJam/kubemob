using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.AccountManagement
{
    [Preserve(AllMembers = true)]
    public class CloudEnvironment
    {
        public CloudEnvironment(
            string id,
            string name,
            bool isDefault = false)
        {
            this.Id = id;
            this.Name = name;
            this.IsDefault = isDefault;
        }

        public string Id { get; }

        public string Name { get; }

        public bool IsDefault { get; }
    }
}
