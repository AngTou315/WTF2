namespace Ease.Event
{
    public abstract class BaseEventArgs
    {
        //是所有事件参数的基类，约束所有子类都有一个ID属性
        public abstract int ID { get; }
    }
}