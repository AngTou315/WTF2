using Ease.Event;

namespace AAAShare.BsPublic.Event
{
    public class ExpmUpdateEventArgs : BaseEventArgs
    {
        public static int EventID => typeof(ExpmUpdateEventArgs).GetHashCode();
        public override int ID => EventID;
    }
}