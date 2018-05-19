using System;
using Xamarin.Forms;

namespace KubeMob.Common.Behaviors.Base
{
    /// <summary>
    /// Based off <see cref="https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/behaviors/reusable/event-to-command-behavior"/>
    /// </summary>
    /// <typeparam name="T">The type to attach a behaviour to.</typeparam>
    public class BindableBehavior<T> : Behavior<T>
        where T : BindableObject
    {
        public T AssociatedObject
        {
            get;
            private set;
        }

        protected override void OnAttachedTo(T bindable)
        {
            base.OnAttachedTo(bindable);

            this.AssociatedObject = bindable;

            if (bindable.BindingContext != null)
            {
                this.BindingContext = bindable.BindingContext;
            }

            bindable.BindingContextChanged += this.OnBindingContextChanged;
        }

        protected override void OnDetachingFrom(T bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.BindingContextChanged -= this.OnBindingContextChanged;

            this.AssociatedObject = null;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            this.BindingContext = this.AssociatedObject.BindingContext;
        }

        private void OnBindingContextChanged(object sender, EventArgs e) => this.OnBindingContextChanged();
    }
}
