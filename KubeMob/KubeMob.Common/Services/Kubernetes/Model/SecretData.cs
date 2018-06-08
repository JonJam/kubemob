using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class SecretData
    {
        public SecretData(
            string name,
            string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name
        {
            get;
        }

        public string Value
        {
            get;
        }
    }
}
