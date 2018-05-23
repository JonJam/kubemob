using KubeMob.Common.Services.Settings;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels
{
    [Preserve(AllMembers = true)]
    public class SettingsViewModel : ViewModelBase
    {
        // TODO Bind through service
        public SettingsViewModel(IAppSettings settings)
            => this.Settings = settings;

        public IAppSettings Settings
        {
            get;
        }
    }
}