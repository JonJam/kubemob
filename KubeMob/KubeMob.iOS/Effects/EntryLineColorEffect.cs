using System;
using System.Linq;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("KubeMob")]
[assembly: ExportEffect(typeof(KubeMob.iOS.Effects.EntryLineColorEffect), "EntryLineColorEffect")]
namespace KubeMob.iOS.Effects
{
    [Preserve(AllMembers = true)]
    public class EntryLineColorEffect : PlatformEffect
    {
        private UITextField control;

        private CGColor previousBorderColor;
        private nfloat previousCornerRadius;
        private nfloat previousBorderWidth;

        protected override void OnAttached()
        {
            Common.Effects.EntryLineColorEffect effect = (Common.Effects.EntryLineColorEffect)this.Element.Effects.First(e => e is Common.Effects.EntryLineColorEffect);

            this.control = (UITextField)this.Control;
            this.previousBorderColor = this.control.Layer.BorderColor;
            this.previousCornerRadius = this.control.Layer.CornerRadius;
            this.previousBorderWidth = this.control.Layer.BorderWidth;

            this.UpdateLineColor(effect.Color);
        }

        protected override void OnDetached()
        {
            // Removing the effects of this effect.
            this.control.Layer.BorderWidth = this.previousBorderWidth;
            this.control.Layer.CornerRadius = this.previousCornerRadius;
            this.control.Layer.BorderColor = this.previousBorderColor;

            this.control = null;
        }

        private void UpdateLineColor(Color color)
        {
            this.control.Layer.BorderColor = color.ToCGColor();
            this.control.Layer.CornerRadius = 5f;
            this.control.Layer.BorderWidth = 1f;
        }
    }
}