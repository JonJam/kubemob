namespace KubeMob.Common.Behaviors
{
    using System.Linq;
    using Xamarin.Forms;

    // TODO maybe need to change this to entry.
    // TODO Can change this to https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/behaviors/reusable/effect-behavior
    public class LineColorBehavior : Behavior<Entry>//: BindableBehavior<View>
    {
        public static readonly BindableProperty ApplyLineColorProperty = BindableProperty.CreateAttached(
            nameof(LineColorBehavior.ApplyLineColor),
            typeof(bool),
            typeof(LineColorBehavior),
            false,
            propertyChanged: OnApplyLineColorChanged);

        public static readonly BindableProperty LineColorProperty = BindableProperty.CreateAttached(
            nameof(LineColorBehavior.LineColor),
            typeof(Color),
            typeof(LineColorBehavior),
            Color.Default);

        public bool ApplyLineColor
        {
            get => (bool)this.GetValue(LineColorBehavior.ApplyLineColorProperty);
            set => this.SetValue(LineColorBehavior.ApplyLineColorProperty, value);
        }

        public Color LineColor
        {
            get => (Color)this.GetValue(LineColorBehavior.LineColorProperty);
            set => this.SetValue(LineColorBehavior.LineColorProperty, value);
        }

        private static void OnApplyLineColorChanged(
            BindableObject bindable, 
            object oldValue, 
            object newValue)
        {
            var view = bindable as View;

            if (view == null)
            {
                return;
            }

            bool hasLine = (bool)newValue;

            if (hasLine)
            {
                view.Effects.Add(new EntryLineColorEffect());
            }
            else
            {
                var entryLineColorEffectToRemove = view.Effects.FirstOrDefault(e => e is EntryLineColorEffect);
                if (entryLineColorEffectToRemove != null)
                {
                    view.Effects.Remove(entryLineColorEffectToRemove);
                }
            }
        }
    }
}
