using System;
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
            // TODO Clear down this effect
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
                if (this.control != null)
                {
                    this.control.BackgroundTintList = ColorStateList.ValueOf(color.ToAndroid());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}