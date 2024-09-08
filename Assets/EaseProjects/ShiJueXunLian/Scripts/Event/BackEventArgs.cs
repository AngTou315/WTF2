using Ease.Event;

namespace EaseProjects.Template.Event
{
    
    public class BackEventArgs : BaseEventArgs
    {
        //主要目的是生成一个与 BackEventArgs 类型相关的整数值（哈希码）。
        //这个值通常被用作事件系统中的唯一标识符，来区分不同的事件类型。
        public static int EventID => typeof(BackEventArgs).GetHashCode();

        ////重写了 BaseEventArgs 中的 ID 属性，返回 EventID，用来标识 BackEventArgs 事件。
        public override int ID
        {
            get => EventID;
        }
    }
}