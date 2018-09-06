using System;
using Foundation;
using KubeMob.Common;
using UIKit;

namespace KubeMob.iOS
{
    /// <summary>
    /// The UIApplicationDelegate for the application. This class is responsible for launching the
    /// User Interface of the application, as well as listening (and optionally responding) to
    /// application events from iOS.
    /// </summary>
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        // This method is invoked when the application has loaded and is ready to run. In this
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            // Initializing FFImageLoading: https://github.com/luberda-molinet/FFImageLoading/wiki/Xamarin.Forms-API
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();

            global::Xamarin.Forms.Forms.Init();
            this.LoadApplication(new App());

#if ENABLE_TEST_CLOUD
            // Requires Xamarin Test Cloud Agent per: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/deploy-test/uitest-and-test-cloud?tabs=vswin#ios-application-project
            Xamarin.Calabash.Start();
#endif

            return base.FinishedLaunching(uiApplication, launchOptions);

            DateTimeOffset.FromUnixTimeSeconds(Int32.Parse("1"));
        }
    }
}
