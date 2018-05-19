using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Input;
using KubeMob.Common.Behaviors.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Behaviors
{
    /// <summary>
    /// Based off <see cref="https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/behaviors/reusable/event-to-command-behavior"/>
    /// </summary>
    [Preserve(AllMembers = true)]
    public class EventToCommandBehavior : BindableBehavior<VisualElement>
    {
        public static readonly BindableProperty EventNameProperty = BindableProperty.CreateAttached(
            nameof(EventToCommandBehavior.EventName),
            typeof(string),
            typeof(EventToCommandBehavior),
            null);

        public static readonly BindableProperty CommandProperty = BindableProperty.CreateAttached(
            nameof(EventToCommandBehavior.Command),
            typeof(ICommand),
            typeof(EventToCommandBehavior),
            null);

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.CreateAttached(
            nameof(EventToCommandBehavior.CommandParameter),
            typeof(object),
            typeof(EventToCommandBehavior),
            null);

        public static readonly BindableProperty EventArgsConverterProperty = BindableProperty.CreateAttached(
            nameof(EventToCommandBehavior.EventArgsConverter),
            typeof(IValueConverter),
            typeof(EventToCommandBehavior),
            null);

        public static readonly BindableProperty EventArgsConverterParameterProperty = BindableProperty.CreateAttached(
            nameof(EventToCommandBehavior.EventArgsConverterParameter),
            typeof(object),
            typeof(EventToCommandBehavior),
            null);

        private Delegate handler;
        private EventInfo eventInfo;

        public string EventName
        {
            get => (string)this.GetValue(EventToCommandBehavior.EventNameProperty);
            set => this.SetValue(EventToCommandBehavior.EventNameProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)this.GetValue(EventToCommandBehavior.CommandProperty);
            set => this.SetValue(EventToCommandBehavior.CommandProperty, value);
        }

        public object CommandParameter
        {
            get => this.GetValue(EventToCommandBehavior.CommandParameterProperty);
            set => this.SetValue(EventToCommandBehavior.CommandParameterProperty, value);
        }

        public IValueConverter EventArgsConverter
        {
            get => (IValueConverter)this.GetValue(EventToCommandBehavior.EventArgsConverterProperty);
            set => this.SetValue(EventToCommandBehavior.EventArgsConverterProperty, value);
        }

        public object EventArgsConverterParameter
        {
            get => this.GetValue(EventToCommandBehavior.EventArgsConverterParameterProperty);
            set => this.SetValue(EventToCommandBehavior.EventArgsConverterParameterProperty, value);
        }

        protected override void OnAttachedTo(VisualElement bindable)
        {
            base.OnAttachedTo(bindable);

            EventInfo[] events = this.AssociatedObject.GetType().GetRuntimeEvents().ToArray();

            if (!events.Any())
            {
                return;
            }

            this.eventInfo = events.FirstOrDefault(e => e.Name == this.EventName);

            if (this.eventInfo == null)
            {
                throw new ArgumentException($"EventToCommand: Can't find any event named '{this.EventName}' on attached type");
            }

            this.AddEventHandler(this.eventInfo, this.AssociatedObject, this.OnFired);
        }

        protected override void OnDetachingFrom(VisualElement bindable)
        {
            if (this.handler != null)
            {
                this.eventInfo.RemoveEventHandler(this.AssociatedObject, this.handler);
            }

            base.OnDetachingFrom(bindable);
        }

        private void AddEventHandler(EventInfo info, object item, Action<object, EventArgs> action)
        {
            ParameterExpression[] eventParameters = info.EventHandlerType
                .GetRuntimeMethods().First(m => m.Name == "Invoke")
                .GetParameters()
                .Select(p => Expression.Parameter(p.ParameterType))
                .ToArray();

            MethodInfo actionInvoke = action.GetType()
                .GetRuntimeMethods().First(m => m.Name == "Invoke");

            this.handler = Expression.Lambda(
                info.EventHandlerType,
                Expression.Call(Expression.Constant(action), actionInvoke, eventParameters[0], eventParameters[1]),
                eventParameters).Compile();

            info.AddEventHandler(item, this.handler);
        }

        private void OnFired(object sender, EventArgs eventArgs)
        {
            if (this.Command == null)
            {
                return;
            }

            object parameter = this.CommandParameter;

            if (eventArgs != null &&
                eventArgs != EventArgs.Empty)
            {
                parameter = eventArgs;

                if (this.EventArgsConverter != null)
                {
                    parameter = this.EventArgsConverter.Convert(eventArgs, typeof(object), this.EventArgsConverterParameter, CultureInfo.CurrentUICulture);
                }
            }

            if (this.Command.CanExecute(parameter))
            {
                this.Command.Execute(parameter);
            }
        }
    }
}