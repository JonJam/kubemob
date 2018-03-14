using System.Globalization;
using System.Threading;
using Android.Runtime;
using Java.Util;
using KubeMob.Common.Services.Localization;

[assembly: Xamarin.Forms.Dependency(typeof(KubeMob.Droid.Services.Localization.Localize))]

namespace KubeMob.Droid.Services.Localization
{
    /// <summary>
    /// Based on <see cref="https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/localization?tabs=vswin#android-application-project"/>
    /// </summary>
    public class Localize : ILocalize
    {
        [Preserve()]
        public Localize()
        {
        }

        public void SetLocale(CultureInfo ci)
        {
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }

        public CultureInfo GetCurrentCultureInfo()
        {
            string netLanguage = "en";
            Locale androidLocale = Java.Util.Locale.Default;
            netLanguage = Localize.AndroidToDotNetLanguage(androidLocale.ToString().Replace("_", "-"));

            // This gets called a lot - try/catch can be expensive so consider caching or something.
            CultureInfo ci;

            try
            {
                ci = new CultureInfo(netLanguage);
            }
            catch (CultureNotFoundException)
            {
                // Android locale not valid .NET culture (eg. "en-ES" : English in Spain)
                // fallback to first characters, in this case "en"
                try
                {
                    string fallback = Localize.ToDotNetFallbackLanguage(new PlatformCulture(netLanguage));
                    ci = new CultureInfo(fallback);
                }
                catch (CultureNotFoundException)
                {
                    // Android language not valid .NET culture, falling back to English
                    ci = new System.Globalization.CultureInfo("en");
                }
            }
            return ci;
        }

        private static string AndroidToDotNetLanguage(string androidLanguage)
        {
            string netLanguage = androidLanguage;

            // Certain languages need to be converted to CultureInfo equivalent.
            switch (androidLanguage)
            {
                // "Malaysian (Brunei)" not supported .NET culture.
                // "Malaysian (Malaysia)" not supported .NET culture.
                // "Malaysian (Singapore)" not supported .NET culture.
                case "ms-BN":   
                case "ms-MY":   
                case "ms-SG":
                    // Closest supported
                    netLanguage = "ms"; 
                    break;
                // "Indonesian (Indonesia)" has different code in  .NET
                case "in-ID":
                    // Correct code for .NET
                    netLanguage = "id-ID";
                    break;
                // "Schwiizertüütsch (Swiss German)" not supported .NET culture
                case "gsw-CH":
                    // Closest supported
                    netLanguage = "de-CH"; 
                    break;
                // TODO add more application-specific cases here (if required). ONLY use cultures that have been tested and known to work.
            }
            return netLanguage;
        }

        private static string ToDotNetFallbackLanguage(PlatformCulture platCulture)
        {
            // Use the first part of the identifier (two chars, usually);
            string netLanguage = platCulture.LanguageCode;

            switch (platCulture.LanguageCode)
            {
                case "gsw":
                    // Equivalent to German (Switzerland) for this app.
                    netLanguage = "de-CH";
                    break;
                // TODO add more application-specific cases here (if required). ONLY use cultures that have been tested and known to work.
            }

            return netLanguage;
        }
    }
}