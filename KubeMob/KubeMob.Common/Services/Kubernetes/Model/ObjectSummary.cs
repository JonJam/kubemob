using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class ObjectSummary
    {
        public ObjectSummary(
            string name)
            : this(name, string.Empty)
        {
        }

        public ObjectSummary(
            string name,
            string description)
        {
            this.Name = name;
            this.Description = description;
        }

        public string Name
        {
            get;
        }

        public string Description
        {
            get;
        }
    }
}
