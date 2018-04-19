using KubeMob.Common.Views;
using KubeMob.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NoHelpEntry), typeof(NoHelpEntryRenderer))]
namespace KubeMob.iOS.Renderers
{
    /// <summary>
    /// Entry renderer which disables auto-capatialisation on iOS. Based off <see cref="https://developer.xamarin.com/recipes/cross-platform/xamarin-forms/ios/prevent-keyboard-suggestions/"/>.
    /// </summary>
    public class NoHelpEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (this.Control == null)
            {
                return;
            }

            this.Control.SpellCheckingType = UITextSpellCheckingType.No;
            this.Control.AutocorrectionType = UITextAutocorrectionType.No;
            this.Control.AutocapitalizationType = UITextAutocapitalizationType.None;
        }
    }
}