using Xamarin.Forms.Internals;

namespace KubeMob.Common.Behaviors
{
    using Xamarin.Forms;

    /// <summary>
    /// Based on https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/behaviors/reusable/effect-behavior
    /// </summary>
    [Preserve(AllMembers = true)]
    public class EffectBehavior : Behavior<View>
    {
        public static readonly BindableProperty ApplyEffectProperty = BindableProperty.CreateAttached(
            nameof(EffectBehavior.ApplyEffect),
            typeof(bool),
            typeof(EffectBehavior),
            false,
            propertyChanged: EffectBehavior.OnApplyEffectChanged);

        public static readonly BindableProperty EffectProperty = BindableProperty.CreateAttached(
            nameof(EffectBehavior.Effect),
            typeof(Effect),
            typeof(EffectBehavior),
            null,
            propertyChanged: EffectBehavior.OnEffectChanged);

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

            this.AddEffect(this.ApplyEffect);
        }

        protected override void OnDetachingFrom(BindableObject bindable)
        {
            this.RemoveEffect();

            this.view = null;

            base.OnDetachingFrom(bindable);
        }

        private static void OnApplyEffectChanged(
            BindableObject bindable,
            object oldValue,
            object newValue)
        {
            EffectBehavior behavior = (EffectBehavior)bindable;
            bool applyEffect = (bool)newValue;

            if (applyEffect)
            {
                behavior.AddEffect(applyEffect);
            }
            else
            {
                behavior.RemoveEffect();
            }
        }

        private static void OnEffectChanged(
            BindableObject bindable,
            object oldValue,
            object newValue)
        {
            EffectBehavior behavior = (EffectBehavior)bindable;
            bool applyEffect = behavior.ApplyEffect;

            if (applyEffect)
            {
                behavior.AddEffect(applyEffect);
            }
            else
            {
                behavior.RemoveEffect();
            }
        }

        private void AddEffect(
            bool applyEffect)
        {
            Effect effectToApply = this.Effect;

            if (this.view != null &&
                effectToApply != null &&
                applyEffect)
            {
                this.view.Effects.Add(effectToApply);
            }
        }

        private void RemoveEffect()
        {
            Effect effectToApply = this.Effect;

            if (this.view != null && effectToApply != null)
            {
                this.view.Effects.Remove(effectToApply);
            }
        }
    }
}