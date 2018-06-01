using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class EnvironmentVariable
    {
        public EnvironmentVariable(
            string name,
            string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name
        {
            get;
        }

        public string Value
        {
            get;
        }
    }
}
