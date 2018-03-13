using KubeMob.Common.Services.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace KubeMob.Common
{
    public partial class App : Application
	{
        public App() => this.InitializeComponent();

        protected override async void OnStart ()
		{
		    INavigationService navigationService = ViewModelLocator.Resolve<INavigationService>();

		    await navigationService.Initialize();
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
