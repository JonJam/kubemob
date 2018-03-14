using System.Globalization;

namespace KubeMob.Common.Services.Localization
{
    /// <summary>
    /// Based off <see cref="https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/localization?tabs=vswin"/>
    /// </summary>
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();

        void SetLocale(CultureInfo ci);
    }
}
