using Android.Widget;
using eShopOnContainers.Core.Behaviors;
using eShopOnContainers.Droid.Effects;
using System;
using System.ComponentModel;
using KubeMob.Common.Behaviors;
using KubeMob.Common.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

// TODO tidy this up
[assembly: ResolutionGroupName("KubeMob")]
[assembly: ExportEffect(typeof(EntryLineColorEffect), "EntryLineColorEffect")]
namespace KubeMob.Droid.Effects
{
    public class EntryLineColorEffect : PlatformEffect
    {
        EditText control;

        protected override void OnAttached()
        {
            try
            {
                control = Control as EditText;
                UpdateLineColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {
            control = null;
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            if (args.PropertyName == LineColorBehavior.LineColorProperty.PropertyName)
            {
                UpdateLineColor();
            }
        }

        private void UpdateLineColor()
        {
            try
            {
                if (control != null)
                {
                    control.Background.SetColorFilter(LineColorBehavior.GetLineColor(Element).ToAndroid(), Android.Graphics.PorterDuff.Mode.SrcAtop);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}