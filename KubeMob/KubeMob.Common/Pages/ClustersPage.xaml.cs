﻿using KubeMob.Common.Pages.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Pages
{
    public partial class ClustersPage : ExtendedContentPage
    {
        [Preserve]
        public ClustersPage()
        {
            this.InitializeComponent();

            // TODO Try refactor this into ViewModel ??
            this.Clusters.ItemSelected += this.OnSelected;
            this.Accounts.ItemSelected += this.OnSelected;
        }

        private void OnSelected(object sender, SelectedItemChangedEventArgs e) => ((ListView)sender).SelectedItem = null;
    }
}