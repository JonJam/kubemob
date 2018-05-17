using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes
{
    [Preserve(AllMembers = true)]
    public class PersistentVolumeClaimsSummary
    {
        public PersistentVolumeClaimsSummary(
            string name,
            string status)
        {
            this.Name = name;
            this.Status = status;
        }

        public string Name
        {
            get;
        }

        public string Status
        {
            get;
        }
    }
}
