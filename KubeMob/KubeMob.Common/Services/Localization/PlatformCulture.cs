using System;

namespace KubeMob.Common.Services.Localization
{
    /// <summary>
    /// Helper class for splitting locales like iOS: ms_MY, gsw_CH and Android: in-ID
    /// into parts so we can create a .NET culture (or fallback culture)
    /// 
    /// Based off <see cref="https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/localization?tabs=vswin#displaying-the-correct-language"/>
    /// </summary>
    public class PlatformCulture
    {
        public PlatformCulture(string platformCultureString)
        {
            if (string.IsNullOrEmpty(platformCultureString))
            {
                throw new ArgumentException("Expected culture identifier", nameof(platformCultureString));
            }

            // .NET expects dash, not underscore
            this.PlatformString = platformCultureString.Replace("_", "-"); 

            int dashIndex = this.PlatformString.IndexOf("-", StringComparison.Ordinal);

            if (dashIndex > 0)
            {
                string[] parts = this.PlatformString.Split('-');

                this.LanguageCode = parts[0];
                this.LocaleCode = parts[1];
            }
            else
            {
                this.LanguageCode = this.PlatformString;
                this.LocaleCode = string.Empty;
            }
        }

        public string PlatformString
        {
            get;
        }

        public string LanguageCode
        {
            get;
        }

        public string LocaleCode
        {
            get;
        }

        public override string ToString() => this.PlatformString;
    }
}
