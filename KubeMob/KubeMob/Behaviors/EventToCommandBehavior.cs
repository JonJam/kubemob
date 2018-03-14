using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Input;
using KubeMob.Common.Behaviors.Base;
using Xamarin.Forms;

namespace KubeMob.Common.Behaviors
{
    /// <summary>
    /// Based off <see cref="https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/behaviors/reusable/event-to-command-behavior"/>
    /// </summary>
    public class EventToCommandBehavior : BindableBehavior<View>
    {
        public static BindableProperty EventNameProperty = BindableProperty.CreateAttached(
            "EventName",
            typeof(string),
            typeof(EventToCommandBehavior),
            null);

        public static BindableProperty CommandProperty = BindableProperty.CreateAttached(
            "Command",
            typeof(ICommand),
            typeof(EventToCommandBehavior),
            null);

        public static BindableProperty CommandParameterProperty = BindableProperty.CreateAttached(
            "CommandParameter",
            typeof(object),
            typeof(EventToCommandBehavior),
            null);

        public static BindableProperty EventArgsConverterProperty = BindableProperty.CreateAttached(
            "EventArgsConverter",
            typeof(IValueConverter),
            typeof(EventToCommandBehavior),
            null);

        public static BindableProperty EventArgsConverterParameterProperty = BindableProperty.CreateAttached(
            "EventArgsConverterParameter",
            typeof(object),
            typeof(EventToCommandBehavior),
            null);
        
        private EventInfo eventInfo;

        protected Delegate Handler;
        
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

        protected override void OnAttachedTo(View visualElement)
        {
            base.OnAttachedTo(visualElement);

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

        protected override void OnDetachingFrom(View view)
        {
            if (this.Handler != null)
            {
                this.eventInfo.RemoveEventHandler(this.AssociatedObject, this.Handler);
            }

            base.OnDetachingFrom(view);
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

            this.Handler = Expression.Lambda(
                info.EventHandlerType,
                Expression.Call(Expression.Constant(action), actionInvoke, eventParameters[0], eventParameters[1]),
                eventParameters
            )
            .Compile();

            info.AddEventHandler(item, this.Handler);
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