using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes
{
    [Preserve(AllMembers = true)]
    public class SecretSummary
    {
        public SecretSummary(
            string name,
            string type)
        {
            this.Name = name;
            this.Type = type;
        }

        public string Name
        {
            get;
        }

        public string Type
        {
            get;
        }
    }
}
