using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KubeMob.Common.Pages
{
    public partial class ClusterMasterPage : ContentPage
    {
        /*public ListView ListView*/

        public ClusterMasterPage()
        {
            InitializeComponent();

            //BindingContext = new ClusterPageMasterViewModel();
            //ListView = MenuItemsListView;
        }

        //class ClusterPageMasterViewModel : INotifyPropertyChanged
        //{
        //    public ObservableCollection<ClusterPageMenuItem> MenuItems { get; set; }
            
        //    public ClusterPageMasterViewModel()
        //    {
        //        MenuItems = new ObservableCollection<ClusterPageMenuItem>(new[]
        //        {
        //            new ClusterPageMenuItem { Id = 0, Title = "Page 1" },
        //            new ClusterPageMenuItem { Id = 1, Title = "Page 2" },
        //            new ClusterPageMenuItem { Id = 2, Title = "Page 3" },
        //            new ClusterPageMenuItem { Id = 3, Title = "Page 4" },
        //            new ClusterPageMenuItem { Id = 4, Title = "Page 5" },
        //        });
        //    }
            
        //    #region INotifyPropertyChanged Implementation
        //    public event PropertyChangedEventHandler PropertyChanged;
        //    void OnPropertyChanged([CallerMemberName] string propertyName = "")
        //    {
        //        if (PropertyChanged == null)
        //            return;

        //        PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //    #endregion
        //}


    }
}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace KubeMob.Common.Pages
//{

//    public class ClusterPageMenuItem
//    {
//        public ClusterPageMenuItem()
//        {
//            TargetType = typeof(ClusterPageDetail);
//        }
//        public int Id { get; set; }
//        public string Title { get; set; }

//        public Type TargetType { get; set; }
//    }
//}