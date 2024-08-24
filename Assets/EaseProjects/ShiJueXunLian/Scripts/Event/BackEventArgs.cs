using Ease.Event;

namespace EaseProjects.Template.Event
{
    public class BackEventArgs : BaseEventArgs
    {
        public static int EventID => typeof(BackEventArgs).GetHashCode();

        public override int ID
        {
            get => EventID;
        }
    }
}