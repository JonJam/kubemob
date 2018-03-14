using System.Globalization;
using KubeMob.Common.Services.Localization;
using KubeMob.Common.Services.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace KubeMob.Common
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();

            // Setting language display in following: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/localization?tabs=vswin#displaying-the-correct-language
            CultureInfo ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            // Set the RESX for resource localization
            Resx.AppResources.Culture = ci;
            // Set the Thread for locale-aware methods
            DependencyService.Get<ILocalize>().SetLocale(ci); 
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
