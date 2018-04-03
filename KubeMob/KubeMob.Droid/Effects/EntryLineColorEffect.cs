using System;
using System.Linq;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Android;

// TODO tidy this up
[assembly: ResolutionGroupName("KubeMob")]
[assembly: ExportEffect(typeof(KubeMob.Droid.Effects.EntryLineColorEffect), "EntryLineColorEffect")]
namespace KubeMob.Droid.Effects
{
    [Preserve(AllMembers = true)]
    public class EntryLineColorEffect : PlatformEffect
    {
        EditText control;
        //private ColorFilter originalBackground;


        protected override void OnAttached()
        {
            try
            {
                Common.Effects.EntryLineColorEffect effect = (Common.Effects.EntryLineColorEffect)this.Element.Effects.FirstOrDefault(e => e is Common.Effects.EntryLineColorEffect);

                // TODO Null checks
                this.control = this.Control as EditText;
                //this.originalBackground = this.control.Background.ColorFilter;

                this.UpdateLineColor(effect.Color);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {
            //control.Background.SetColorFilter(this.originalBackground);
            control = null;
        }

        //protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        //{
        //    if (args.PropertyName == LineColorBehavior.LineColorProperty.PropertyName)
        //    {
        //        UpdateLineColor();
        //    }
        //}

        private void UpdateLineColor(Xamarin.Forms.Color color)
        {
            try
            {
                if (control != null)
                {
                    control.Background.SetColorFilter(color.ToAndroid(), Android.Graphics.PorterDuff.Mode.SrcAtop);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}