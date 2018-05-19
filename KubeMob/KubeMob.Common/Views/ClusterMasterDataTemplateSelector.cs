using KubeMob.Common.ViewModels.MasterDetail;
using Xamarin.Forms;

namespace KubeMob.Common.Views
{
    public class ClusterMasterDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NamespaceSelectorDataTemplate
        {
            get;
            set;
        }

        public DataTemplate OverviewDataTemplate
        {
            get;
            set;
        }

        public DataTemplate ObjectTypeDataTemplate
        {
            get;
            set;
        }

        protected override DataTemplate OnSelectTemplate(
            object item,
            BindableObject container)
        {
            switch (item)
            {
                case OverviewMenuItemViewModel _:
                    return this.OverviewDataTemplate;
                case NamespaceSelectorViewModel _:
                    return this.NamespaceSelectorDataTemplate;
                default:
                    return this.ObjectTypeDataTemplate;
            }
        }
    }
}
