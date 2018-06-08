using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class Capacity
    {
        public Capacity(
            string resourceName,
            string quantity)
        {
            this.ResourceName = resourceName;
            this.Quantity = quantity;
        }

        public string ResourceName
        {
            get;
        }

        public string Quantity
        {
            get;
        }
    }
}
