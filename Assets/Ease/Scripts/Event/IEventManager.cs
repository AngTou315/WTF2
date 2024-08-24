using System;
using Ease.Core;

namespace Ease.Event
{
    public interface IEventManager: IModule,ILife
    {
        void Bind(int id, EventHandler<BaseEventArgs> callback);
        void UnBind(int id, EventHandler<BaseEventArgs> callback);
        void Fire(object sender, BaseEventArgs eventArgs);
        void FireNow(object sender, BaseEventArgs eventArgs);
    }
}