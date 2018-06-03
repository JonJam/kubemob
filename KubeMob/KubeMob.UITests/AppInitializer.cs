using Xamarin.UITest;

namespace KubeMob.UITests
{
    public static class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
                    .ApkFile(@"C:\Projects\kubemob\KubeMob\KubeMob.Droid\bin\Release\com.jonjam.kubemob.apk")
                    .StartApp();
            }

            return ConfigureApp
                .iOS
                .StartApp();
        }
    }
}