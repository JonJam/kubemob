using System.Globalization;
using KubeMob.Common.Services.Localization;
using KubeMob.Common.Services.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace KubeMob.Common
{
    [Preserve(AllMembers = true)]
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();

            // Setting language display in following: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/localization?tabs=vswin#displaying-the-correct-language
            ILocalize localize = ViewModelLocator.Resolve<ILocalize>();
            CultureInfo ci = localize.GetCurrentCultureInfo();

            // Set the RESX for resource localization
            Resx.AppResources.Culture = ci;

            // Set the Thread for locale-aware methods
            localize.SetLocale(ci);
        }

        protected override async void OnStart()
        {
            INavigationService navigationService = ViewModelLocator.Resolve<INavigationService>();

            await navigationService.Initialize();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
