using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Effects
{
    [Preserve(AllMembers = true)]
    public class EntryLineColorEffect : RoutingEffect
    {
        public EntryLineColorEffect() : base("KubeMob.EntryLineColorEffect")
        {
        }

        public Color Color { get; set; }
    }
}
