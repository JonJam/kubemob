using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using KubeMob.Common;
using Xamarin.Forms;

namespace KubeMob.Droid
{
    /// <summary>
    /// Material design Xamarin Forms: https://developer.xamarin.com/guides/xamarin-forms/platform-features/android/
    ///
    /// Xamarin Essentials setup: https://docs.microsoft.com/en-us/xamarin/essentials/get-started?context=xamarin%2Fios&tabs=windows%2Candroid#installation
    /// </summary>
    [Activity(Label = "kubemob", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            // Enable fast renderers: https://developer.xamarin.com/guides/xamarin-forms/under-the-hood/fast-renderers/
            Forms.SetFlags("FastRenderers_Experimental");

            // Initializing Xamarin.Essentials.
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            // Initializing FFImageLoading: https://github.com/luberda-molinet/FFImageLoading/wiki/Xamarin.Forms-API
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            this.LoadApplication(new App());
        }
    }
}