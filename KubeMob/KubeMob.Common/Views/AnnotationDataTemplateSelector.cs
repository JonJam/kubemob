using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KubeMob.Common.Views
{
    public class AnnotationDataTemplateSelector : DataTemplateSelector
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
            string annotation = item as string;




        }
    }
}
