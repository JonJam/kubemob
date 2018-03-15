
using Android.App;
using Android.Content.PM;
using Android.OS;
using KubeMob.Common;
using Xamarin.Forms;

namespace KubeMob.Droid
{
    // Material design Xamarin Forms: https://developer.xamarin.com/guides/xamarin-forms/platform-features/android/
    [Activity(Label = "KubeMob", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            // Enable fast renderers: https://developer.xamarin.com/guides/xamarin-forms/under-the-hood/fast-renderers/
            Forms.SetFlags("FastRenderers_Experimental");

            global::Xamarin.Forms.Forms.Init(this, bundle);
            this.LoadApplication(new App());
        }
    }
}

