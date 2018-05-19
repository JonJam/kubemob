using KubeMob.Common.ViewModels.MasterDetail;
using Xamarin.Forms;

namespace KubeMob.Common.Views
{
    public class ClusterMasterDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ObjectTypeDataTemplate
        {
            get;
            set;
        }

        public DataTemplate OverviewDataTemplate
        {
            get;
            set;
        }

        protected override DataTemplate OnSelectTemplate(
            object item,
            BindableObject container)
        {
            if (item is ObjectTypeMenuItemViewModel)
            {
                return this.ObjectTypeDataTemplate;
            }
            else
            {
                return this.OverviewDataTemplate;
            }
        }
    }
}
