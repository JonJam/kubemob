using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class Namespace
    {
        public Namespace(
            string name) => this.Name = name;

        public string Name
        {
            get;
        }
    }
}
