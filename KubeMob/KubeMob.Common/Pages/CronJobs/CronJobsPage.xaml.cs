using KubeMob.Common.Pages.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Pages.CronJobs
{
    public partial class CronJobsPage : ExtendedContentPage
    {
        [Preserve]
        public CronJobsPage()
        {
            this.InitializeComponent();
            this.CronJobs.ItemSelected += this.OnCronJobSelected;
        }

        private void OnCronJobSelected(object sender, SelectedItemChangedEventArgs e) => this.CronJobs.SelectedItem = null;
    }
}