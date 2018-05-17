using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class IngressSummary
    {
        public IngressSummary(
            string name)
        {
            this.Name = name;
        }

        public string Name
        {
            get;
        }
    }
}
