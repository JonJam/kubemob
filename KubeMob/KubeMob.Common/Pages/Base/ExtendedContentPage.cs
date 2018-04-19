using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace KubeMob.Common.Pages.Base
{
    public class ExtendedContentPage : ContentPage
    {
        public ExtendedContentPage()
        {
            // Support for iPhone X: https://blog.xamarin.com/making-ios-11-even-easier-xamarin-forms/
            this.On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }
    }
}
