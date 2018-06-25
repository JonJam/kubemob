using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class ObjectReference
    {
        public ObjectReference(
            string name,
            string kind)
            : this(name, string.Empty, kind)
        {
        }

        public ObjectReference(
            string name,
            string namespaceName,
            string kind)
        {
            this.Name = name;
            this.NamespaceName = namespaceName;
            this.Kind = kind;
        }

        public string Name
        {
            get;
        }

        public string NamespaceName
        {
            get;
        }

        public string Kind
        {
            get;
        }

        public string Summary => !string.IsNullOrWhiteSpace(this.NamespaceName)
            ? $"{this.NamespaceName}/{this.Name}"
            : $"{this.Kind}/{this.Name}";
    }
}