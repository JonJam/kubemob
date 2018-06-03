using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    // TODO Remove properties that do not display
    [Preserve(AllMembers = true)]
    public class PodCondition
    {
        public PodCondition(
            string type,
            string status,
            string lastHeartbeatTime,
            string lastTransitionTime,
            string reason)
        {
            this.Type = type;
            this.Status = status;
            this.LastHeartbeatTime = lastHeartbeatTime;
            this.LastTransitionTime = lastTransitionTime;
            this.Reason = reason;
        }

        public string Type
        {
            get;
        }

        public string Status
        {
            get;
        }

        public string LastHeartbeatTime
        {
            get;
        }

        public string LastTransitionTime
        {
            get;
        }

        public string Reason
        {
            get;
        }
    }
}
