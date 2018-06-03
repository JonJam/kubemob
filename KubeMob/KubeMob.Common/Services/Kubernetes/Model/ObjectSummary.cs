using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class ObjectSummary
    {
        public ObjectSummary(
            string name,
            string namespaceName)
            : this(name, namespaceName, string.Empty)
        {
        }

        public ObjectSummary(
            string name,
            string namespaceName,
            string description)
        {
            this.Name = name;
            this.NamespaceName = namespaceName;
            this.Description = description;
        }

        public string Name
        {
            get;
        }

        public string NamespaceName
        {
            get;
        }

        public string Description
        {
            get;
        }
    }
}
