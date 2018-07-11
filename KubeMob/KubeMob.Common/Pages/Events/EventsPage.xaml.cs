using KubeMob.Common.Pages.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Pages.Events
{
    public partial class EventsPage : ExtendedContentPage
    {
        [Preserve]
        public EventsPage() => this.InitializeComponent();

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}