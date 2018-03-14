using Xamarin.Forms;

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
        }

        public ExtendedNavigationPage(Page root) : base(root)
        {
            this.InitializeComponent();
        }
    }
}