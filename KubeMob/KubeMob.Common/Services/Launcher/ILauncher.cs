using System;

namespace KubeMob.Common.Services.Launcher
{
    public interface ILauncher
    {
        void LaunchBrowser(Uri uri);
    }
}
