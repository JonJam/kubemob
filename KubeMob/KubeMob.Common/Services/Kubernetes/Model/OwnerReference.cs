using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class OwnerReference
    {
        public OwnerReference(
            string name,
            string kind)
        {
            this.Name = name;
            this.Kind = kind;
        }

        public string Name
        {
            get;
        }

        public string Kind
        {
            get;
        }
    }
}
