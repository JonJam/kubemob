using System;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class Event
    {
        public Event(
            string message,
            DateTime lastSeenDateTime,
            string lastSeen)
        {
            this.Message = message;
            this.LastSeenDateTime = lastSeenDateTime;
            this.LastSeen = lastSeen;
        }

        public string Message
        {
            get;
        }

        public DateTime LastSeenDateTime
        {
            get;
        }

        public string LastSeen
        {
            get;
        }
    }
}
