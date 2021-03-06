using KubeMob.Common.Services.Kubernetes.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Views
{
    [Preserve(AllMembers = true)]
    public class MetadataItemDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Basic
        {
            get;
            set;
        }

        public DataTemplate Popup
        {
            get;
            set;
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            MetadataItem annotation = (MetadataItem)item;

            bool isValueJson = true;

            try
            {
                JObject.Parse(annotation.Value);
            }
            catch (JsonReaderException)
            {
                isValueJson = false;
            }

            return isValueJson ? this.Popup : this.Basic;
        }
    }
}
