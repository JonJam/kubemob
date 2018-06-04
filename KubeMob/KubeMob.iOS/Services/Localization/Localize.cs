using System.Globalization;
using System.Threading;
using Foundation;
using KubeMob.Common.Services.Localization;

[assembly: Xamarin.Forms.Dependency(typeof(KubeMob.iOS.Services.Localization.Localize))]
namespace KubeMob.iOS.Services.Localization
{
    /// <summary>
    /// Based off <see cref="https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/localization?tabs=vswin#ios-application-project"/>
    /// </summary>
    public class Localize : ILocalize
    {
        [Preserve]
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

            if (NSLocale.PreferredLanguages.Length > 0)
            {
                string pref = NSLocale.PreferredLanguages[0];

                netLanguage = Localize.ToDotNetLanguage(pref);
            }

            // This gets called a lot - try/catch can be expensive so consider caching or something.
            CultureInfo ci;

            try
            {
                ci = new CultureInfo(netLanguage);
            }
            catch (CultureNotFoundException)
            {
                // iOS locale not valid .NET culture (eg. "en-ES" : English in Spain)
                // fallback to first characters, in this case "en".
                try
                {
                    string fallback = Localize.ToDotNetFallbackLanguage(new PlatformCulture(netLanguage));
                    ci = new CultureInfo(fallback);
                }
                catch (CultureNotFoundException)
                {
                    // iOS language not valid .NET culture, falling back to English.
                    ci = new CultureInfo("en");
                }
            }

            return ci;
        }

        private static string ToDotNetLanguage(string iosLanguage)
        {
            string netLanguage = iosLanguage;

            // Certain languages need to be converted to CultureInfo equivalent
            // TODO add more application-specific cases here (if required). ONLY use cultures that have been tested and known to work.
            switch (iosLanguage)
            {
                // "Malaysian (Malaysia)" not supported .NET culture
                // "Malaysian (Singapore)" not supported .NET culture
                case "ms-MY":
                case "ms-SG":
                    // Closest supported
                    netLanguage = "ms";
                    break;

                // "Schwiizertüütsch (Swiss German)" not supported .NET culture
                case "gsw-CH":
                    // Closest supported
                    netLanguage = "de-CH";
                    break;
            }

            return netLanguage;
        }

        private static string ToDotNetFallbackLanguage(PlatformCulture platCulture)
        {
            // Use the first part of the identifier (two chars, usually)
            string netLanguage = platCulture.LanguageCode;

            switch (platCulture.LanguageCode)
            {
                case "pt":
                    // Fallback to Portuguese (Portugal).
                    netLanguage = "pt-PT";
                    break;
                case "gsw":
                    // Equivalent to German (Switzerland) for this app.
                    netLanguage = "de-CH";
                    break;
            }

            return netLanguage;
        }
    }
}