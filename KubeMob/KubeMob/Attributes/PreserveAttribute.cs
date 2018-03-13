using System;

namespace KubeMob.Attributes
{
    /// <summary>
    /// Attribute used to preserve code that would be removed by iOS/Android linker <see cref="https://developer.xamarin.com/guides/ios/deployment,_testing,_and_metrics/linker/#Preserving_Code"/>.
    /// </summary>
    public sealed class PreserveAttribute : Attribute
    {
        public bool AllMembers;
        public bool Conditional;
    }
}
