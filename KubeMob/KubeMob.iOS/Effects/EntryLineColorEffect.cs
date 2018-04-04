using System;
using System.ComponentModel;
using System.Linq;
using CoreAnimation;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.iOS;

// TODO tidy this up
[assembly: ResolutionGroupName("KubeMob")]
[assembly: ExportEffect(typeof(KubeMob.iOS.Effects.EntryLineColorEffect), "EntryLineColorEffect")]
namespace KubeMob.iOS.Effects
{
    [Preserve(AllMembers = true)]
    public class EntryLineColorEffect : PlatformEffect
    {
        UITextField control;

        protected override void OnAttached()
        {
            try
            {
                Common.Effects.EntryLineColorEffect effect = (Common.Effects.EntryLineColorEffect)this.Element.Effects.FirstOrDefault(e => e is Common.Effects.EntryLineColorEffect);

                control = Control as UITextField;

                UpdateLineColor(effect.Color);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can't set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {
            // TODO Clear down this effect
            control = null;
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            if (args.PropertyName == "Height")
            {
                Initialize();

                Common.Effects.EntryLineColorEffect effect = (Common.Effects.EntryLineColorEffect)this.Element.Effects.FirstOrDefault(e => e is Common.Effects.EntryLineColorEffect);
                UpdateLineColor(effect.Color);
            }
        }

        private void Initialize()
        {
            var entry = Element as Entry;
            if (entry != null)
            {
                Control.Bounds = new CGRect(0, 0, entry.Width, entry.Height);
            }
        }

        private void UpdateLineColor(Xamarin.Forms.Color color)
        {
            BorderLineLayer lineLayer = control.Layer.Sublayers.OfType<BorderLineLayer>().FirstOrDefault();

            if (lineLayer == null)
            {
                lineLayer = new BorderLineLayer();
                lineLayer.MasksToBounds = true;
                lineLayer.BorderWidth = 1.0f;
                control.Layer.AddSublayer(lineLayer);
                control.BorderStyle = UITextBorderStyle.None;
            }

            lineLayer.Frame = new CGRect(0f, Control.Frame.Height - 1f, Control.Bounds.Width, 1f);
            lineLayer.BorderColor = color.ToCGColor();
            control.TintColor = control.TextColor;
        }

        private class BorderLineLayer : CALayer
        {
        }
    }
}