using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class Namespace
    {
        public Namespace(
            string name)
            : this(name, false)
        {
        }

        public Namespace(
            string name,
            bool isDefault)
        {
            this.Name = name;
            this.IsDefault = isDefault;
        }

        public string Name
        {
            get;
        }

        public bool IsDefault
        {
            get;
            set;
        }
    }
}
