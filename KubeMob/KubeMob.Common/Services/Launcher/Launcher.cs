using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Launcher
{
    [Preserve(AllMembers = true)]
    public class Launcher : ILauncher
    {
        public void LaunchBrowser(Uri uri) => Device.OpenUri(uri);
    }
}
