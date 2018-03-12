using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace KubeMob.ViewModels
{
    // Based off <see cref="https://developer.xamarin.com/guides/xamarin-forms/enterprise-application-patterns/mvvm/#Updating_Views_in_Response_to_Changes_in_the_Underlying_View_Model_or_Model">
    public abstract class ExtendedBindableObject : BindableObject
    {
        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <typeparam name="T">Type parameter.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        protected void NotifyPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            string name = ExtendedBindableObject.GetMemberInfo(propertyExpression).Name;

            this.OnPropertyChanged(name);
        }

        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <typeparam name="T">Type parameter.</typeparam>
        /// <param name="storage">The storage.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Boolean value.</returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(storage, value))
            {
                return false;
            }

            storage = value;

            this.OnPropertyChanged(propertyName);

            return true;
        }

        private static MemberInfo GetMemberInfo(Expression expression)
        {
            MemberExpression operand;
            LambdaExpression lambdaExpression = (LambdaExpression)expression;

            if (lambdaExpression.Body is UnaryExpression body)
            {
                operand = (MemberExpression)body.Operand;
            }
            else
            {
                operand = (MemberExpression)lambdaExpression.Body;
            }

            return operand.Member;
        }
    }
}
