using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class ObjectSummary
    {
        public ObjectSummary(
            string name,
            string namespaceName)
            : this(name, namespaceName, Status.None)
        {
        }

        public ObjectSummary(
            string name,
            string namespaceName,
            Status status)
        {
            this.Name = name;
            this.NamespaceName = namespaceName;
            this.Status = status;
        }

        public string Name
        {
            get;
        }

        public string NamespaceName
        {
            get;
        }

        public Status Status
        {
            get;
        }

        public bool IsStatusSuccess => this.Status == Status.Success;

        public bool IsStatusError => this.Status == Status.Error;

        // TODO Remove??
        public bool IsStatusPending => this.Status == Status.Pending;
        
        public bool IsStatusUnknown => this.Status == Status.Unknown;

        public bool IsStatusUpdate => this.Status == Status.Update;

        public bool IsStatusWarning => this.Status == Status.Warning;
    }
}
