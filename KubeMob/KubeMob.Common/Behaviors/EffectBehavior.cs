using KubeMob.Common.Behaviors.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Behaviors
{
    using Xamarin.Forms;

    /// <summary>
    /// Based on https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/behaviors/reusable/effect-behavior
    /// </summary>
    [Preserve(AllMembers = true)]
    public class EffectBehavior : BindableBehavior<View>
    {
        public static readonly BindableProperty ApplyEffectProperty = BindableProperty.CreateAttached(
            nameof(EffectBehavior.ApplyEffect),
            typeof(bool),
            typeof(EffectBehavior),
            false,
            propertyChanged: EffectBehavior.OnPropertyChanged);

        public static readonly BindableProperty EffectProperty = BindableProperty.CreateAttached(
            nameof(EffectBehavior.Effect),
            typeof(Effect),
            typeof(EffectBehavior),
            null,
            propertyChanged: EffectBehavior.OnPropertyChanged);

        private View view;

        public bool ApplyEffect
        {
            get => (bool)this.GetValue(EffectBehavior.ApplyEffectProperty);
            set => this.SetValue(EffectBehavior.ApplyEffectProperty, value);
        }

        public Effect Effect
        {
            get => (Effect)this.GetValue(EffectBehavior.EffectProperty);
            set => this.SetValue(EffectBehavior.EffectProperty, value);
        }

        protected override void OnAttachedTo(BindableObject bindable)
        {
            base.OnAttachedTo(bindable);

            this.view = (View)bindable;

            this.AddOrRemoveEffect(this.ApplyEffect);
        }

        protected override void OnDetachingFrom(BindableObject bindable)
        {
            this.AddOrRemoveEffect(false);

            this.view = null;

            base.OnDetachingFrom(bindable);
        }

        private static void OnPropertyChanged(
            BindableObject bindable,
            object oldValue,
            object newValue)
        {
            EffectBehavior behavior = (EffectBehavior)bindable;

            behavior.AddOrRemoveEffect(behavior.ApplyEffect);
        }

        private void AddOrRemoveEffect(
            bool applyEffect)
        {
            if (this.view == null || this.Effect == null)
            {
                return;
            }

            if (applyEffect)
            {
                this.view.Effects.Add(this.Effect);
            }
            else
            {
                this.view.Effects.Remove(this.Effect);
            }
        }
    }
}