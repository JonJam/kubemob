using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class MetadataItem
    {
        public MetadataItem(
            string key,
            string value)
        {
            this.Key = key;
            this.Value = value;
        }

        public string Key
        {
            get;
        }

        public string Value
        {
            get;
        }

        public string Summary => $"{this.Key}: {this.Value}";
    }
}
