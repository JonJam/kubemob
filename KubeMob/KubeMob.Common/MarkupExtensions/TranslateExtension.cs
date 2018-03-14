using System;
using System.Globalization;
using System.Resources;
using KubeMob.Common.Services.Localization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KubeMob.Common.MarkupExtensions
{
    /// <summary>
    /// Based off <see cref="https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/localization?tabs=vswin#localizing-xaml"/>
    /// </summary>
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        private readonly CultureInfo ci;

        public TranslateExtension() => this.ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();

        public string Text
        {
            get;
            set;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (this.Text == null)
            {
                return string.Empty;
            }

            string translation = ViewModelLocator.Resolve<ResourceManager>().GetString(this.Text, this.ci);

            if (translation == null)
            {
#if DEBUG
                throw new ArgumentException(
                    $"Key '{this.Text}' was not found in resources for culture '{this.ci.Name}'.",
                    nameof(this.Text));
#else
				translation = this.Text; // returns the key, which GETS DISPLAYED TO THE USER
#endif
            }

            return translation;
        }
    }
}
