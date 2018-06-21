using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class Condition
    {
        public Condition(
            string type,
            string status,
            string lastHeartbeatTime,
            string lastTransitionTime,
            string reason,
            string message)
        {
            this.Type = type;
            this.Status = status;
            this.LastHeartbeatTime = lastHeartbeatTime;
            this.LastTransitionTime = lastTransitionTime;
            this.Reason = reason;
            this.Message = message;
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

        public string Message
        {
            get;
        }
    }
}
