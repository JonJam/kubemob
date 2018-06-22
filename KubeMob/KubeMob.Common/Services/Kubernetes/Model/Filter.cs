using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class Filter
    {
        public Filter(
            string namespaceName = "",
            string fieldSelector = "",
            string labelSelector = "",
            string other = "")
        {
            this.Namespace = namespaceName;
            this.FieldSelector = fieldSelector;
            this.LabelSelector = labelSelector;
            this.Other = other;
        }

        public string Namespace
        {
            get;
        }

        public string FieldSelector
        {
            get;
        }

        public string LabelSelector
        {
            get;
        }

        public string Other
        {
            get;
        }
    }
}
