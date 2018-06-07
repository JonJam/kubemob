namespace KubeMob.Common.Services.Kubernetes.Model
{
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
