using Ease.Event;
using AAAShare.BsPublic.ObjectInfo;

namespace AAAShare.BsPublic.Event
{
    public class ObjectInfoEventArgs : BaseEventArgs
    {
        public static int EventID => typeof(ObjectInfoEventArgs).GetHashCode();
        public override int ID => EventID;
        
        public ObjectInfo.ObjectInfo objectInfo;

        public bool show = true;
    }
}