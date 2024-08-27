using Ease.Event;

namespace AAAShare.BsPublic.Event
{
    public class ExpmOverEventArgs : BaseEventArgs
    {
        public static int EventID => typeof(ExpmOverEventArgs).GetHashCode();
        public override int ID => EventID;
    }
}