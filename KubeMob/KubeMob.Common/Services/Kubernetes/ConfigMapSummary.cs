using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes
{
    [Preserve(AllMembers = true)]
    public class ConfigMapSummary
    {
        public ConfigMapSummary(
            string name) => this.Name = name;

        public string Name
        {
            get;
        }
    }
}
