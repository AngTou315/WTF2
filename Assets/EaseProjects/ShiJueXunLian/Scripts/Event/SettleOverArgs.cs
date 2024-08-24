using Ease.Event;

namespace EaseProjects.Template.Event
{
    /// <summary>
    /// 结算完毕的界面
    /// </summary>
    public class SettleOverArgs : BaseEventArgs
    {
        public static int EventID => typeof(SettleOverArgs).GetHashCode();

        public override int ID
        {
            get => EventID;
        }
    }
}