using Ease.Event;

namespace AAAShare.BsPublic.Event
{
    public class UICloseEventArgs : BaseEventArgs
    {
        public static int EventID => typeof(UICloseEventArgs).GetHashCode();
        public override int ID => EventID;

        public string uiName;
    }
}