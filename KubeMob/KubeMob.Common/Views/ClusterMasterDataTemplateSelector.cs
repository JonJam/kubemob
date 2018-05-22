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

        public DataTemplate ObjectTypeDataTemplate
        {
            get;
            set;
        }

        public DataTemplate SettingsDataTemplate
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
                case NamespaceSelectorViewModel _:
                    return this.NamespaceSelectorDataTemplate;
                case SettingsMenuItemViewModel _:
                    return this.SettingsDataTemplate;
                default:
                    return this.ObjectTypeDataTemplate;
            }
        }
    }
}
