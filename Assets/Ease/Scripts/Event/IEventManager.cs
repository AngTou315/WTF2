using System;
using Ease.Core;

namespace Ease.Event
{
    //这个接口继承了两个接口：IModule 和 ILife，定义事件系统的核心操作
    //这个接口的目的是让任何实现该接口的类都具有基本的事件绑定、解绑和触发机制。
    public interface IEventManager: IModule,ILife
    {
        void Bind(int id, EventHandler<BaseEventArgs> callback);
        //绑定事件处理器。id 用于标识事件的ID，callback 是事件触发时要执行的回调函数。
        void UnBind(int id, EventHandler<BaseEventArgs> callback);
        //根据ID来解绑
        void Fire(object sender, BaseEventArgs eventArgs);
        //触发事件，延迟执行或存入队列待处理。
        void FireNow(object sender, BaseEventArgs eventArgs);
        //立即触发事件，执行与事件关联的所有回调函数。
    }
}