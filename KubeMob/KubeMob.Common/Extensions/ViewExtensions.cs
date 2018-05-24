using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KubeMob.Common.Extensions
{
    /// <summary>
    ///  Based off <see cref="https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/animation/custom#creating-a-custom-animation-extension-method"/>
    /// </summary>
    public static class ViewExtensions
    {
        private const string ColorAnimationName = "ColorTo";

        public static Task<bool> ColorTo(
            this IAnimatable self,
            Color fromColor,
            Color toColor,
            Action<Color> callback,
            uint length = 250,
            Easing easing = null)
        {
            Color Transform(double t) => Color.FromRgba(
                    fromColor.R + (t * (toColor.R - fromColor.R)),
                    fromColor.G + (t * (toColor.G - fromColor.G)),
                    fromColor.B + (t * (toColor.B - fromColor.B)),
                    fromColor.A + (t * (toColor.A - fromColor.A)));

            return ViewExtensions.ColorAnimation(self, ViewExtensions.ColorAnimationName, Transform, callback, length, easing);
        }

        public static void CancelAnimation(this VisualElement self) => self.AbortAnimation(ViewExtensions.ColorAnimationName);

        private static Task<bool> ColorAnimation(
            IAnimatable element,
            string name,
            Func<double, Color> transform,
            Action<Color> callback,
            uint length,
            Easing easing)
        {
            easing = easing ?? Easing.Linear;

            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();

            element.Animate<Color>(name, transform, callback, 16, length, easing, (v, c) => taskCompletionSource.SetResult(c));

            return taskCompletionSource.Task;
        }
    }
}
