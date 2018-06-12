using System;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class Event
    {
        public Event(
            string message,
            string source,
            string subObject,
            int count,
            string firstSeen,
            DateTime lastSeenDateTime,
            string lastSeen)
        {
            this.Message = message;
            this.Source = source;
            this.SubObject = subObject;
            this.Count = count;
            this.FirstSeen = firstSeen;
            this.LastSeenDateTime = lastSeenDateTime;
            this.LastSeen = lastSeen;
        }

        public string Message
        {
            get;
        }

        public string Source
        {
            get;
        }

        public string SubObject
        {
            get;
        }

        public int Count
        {
            get;
        }

        public string FirstSeen
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
