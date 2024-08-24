using Ease.Event;

namespace EaseProjects.Template.Event
{
    public class ToMainEventArgs : BaseEventArgs
    {
        public static int EventID => typeof(ToMainEventArgs).GetHashCode();

        public override int ID
        {
            get => EventID;
        }
    }
}