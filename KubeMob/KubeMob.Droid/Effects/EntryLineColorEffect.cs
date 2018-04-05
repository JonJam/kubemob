using System.Linq;
using Android.Content.Res;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("KubeMob")]
[assembly: ExportEffect(typeof(KubeMob.Droid.Effects.EntryLineColorEffect), "EntryLineColorEffect")]
namespace KubeMob.Droid.Effects
{
    [Preserve(AllMembers = true)]
    public class EntryLineColorEffect : PlatformEffect
    {
        private EditText control;
        private ColorStateList originalBackgroundTintList;

        protected override void OnAttached()
        {
            Common.Effects.EntryLineColorEffect effect = (Common.Effects.EntryLineColorEffect)this.Element.Effects.First(e => e is Common.Effects.EntryLineColorEffect);

            this.control = (EditText)this.Control;
            this.originalBackgroundTintList = this.control.BackgroundTintList;

            this.UpdateLineColor(effect.Color);
        }

        protected override void OnDetached()
        {
            // Removing the effects of this effect.
            this.control.BackgroundTintList = this.originalBackgroundTintList;

            this.control = null;
        }

        private void UpdateLineColor(Color color) => this.control.BackgroundTintList = ColorStateList.ValueOf(color.ToAndroid());
    }
}