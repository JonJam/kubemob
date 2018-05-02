using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes
{
    [Preserve(AllMembers = true)]
    public class PodSummary
    {
        public PodSummary(
            string name,
            string phase)
        {
            this.Name = name;
            this.Phase = phase;
        }

        public string Name
        {
            get;
        }

        public string Phase
        {
            get;
        }
    }
}
