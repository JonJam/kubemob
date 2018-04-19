using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using NavigationPage = Xamarin.Forms.NavigationPage;
using Page = Xamarin.Forms.Page;

namespace KubeMob.Common.Pages
{
    /// <summary>
    /// Based off <see cref="https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/navigation#handling-navigation-requests"/>
    /// </summary>
    public partial class ExtendedNavigationPage : NavigationPage
    {
        public ExtendedNavigationPage()
        {
            this.InitializeComponent();
            
            // Support for iPhone X: https://blog.xamarin.com/making-ios-11-even-easier-xamarin-forms/
            this.On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        public ExtendedNavigationPage(Page root) : base(root)
        {
            this.InitializeComponent();

            // Support for iPhone X: https://blog.xamarin.com/making-ios-11-even-easier-xamarin-forms/
            this.On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }
    }
}