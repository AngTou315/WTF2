using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Ease.Event
{
    public class FireInfo
    {
        public object sender;
        public BaseEventArgs eventArgs;
    }

    public class DefaultEventManager : IEventManager
    {
        //用于存储事件ID和事件处理程序的映射。每个事件ID可以关联一个或多个事件处理程序（通过多播委托）。
        public Dictionary<int, EventHandler<BaseEventArgs>> dic = new Dictionary<int, EventHandler<BaseEventArgs>>();
        //用于在事件被延迟处理时保存事件的发送者（sender）和事件参数（eventArgs）
        public List<FireInfo> FireCache = new List<FireInfo>();

        public void Bind(int id, EventHandler<BaseEventArgs> callback)//将一个事件处理程序绑定到指定的事件ID上。
        {
            Assert.IsNotNull(callback);

            if (dic.TryGetValue(id, out var handler))
            {
                handler += callback;
            }
            else
            {
                EventHandler<BaseEventArgs> temp = callback;
                dic.Add(id, temp);
            }
        }

        public void UnBind(int id, EventHandler<BaseEventArgs> callback)
        {
            Assert.IsNotNull(callback);
            var remove = false;
            if (dic.TryGetValue(id, out var handler))
            {
                handler -= callback;
                remove = handler == null;
            }
            else
            {
                throw new Exception($"{this.GetType().Name} 解绑失败");
            }

            if (remove)
                dic.Remove(id);
        }

        public void Fire(object sender, BaseEventArgs eventArgs)//将一个事件存储到一个缓存队列中，稍后触发（懒触发）。
        {
            if (FireCache.FirstOrDefault(x => x.sender == sender) != null)
                return;
            FireCache.Add(new FireInfo() { sender = sender, eventArgs = eventArgs });
        }

        public void FireNow(object sender, BaseEventArgs eventArgs)//立即触发某个事件（即时触发）。
        {
            if (dic.TryGetValue(eventArgs.ID, out var handler))
            {
                handler?.Invoke(sender, eventArgs);
            }
            else
            {
                throw new Exception($"{this.GetType().Name} 发射失败");
            }
        }

        public void OnUpdate(float time, float realtime)
        {
            for (int i = 0; i < FireCache.Count; i++)
            {
                var item = FireCache[i];
                FireNow(item.sender, item.eventArgs);
            }
            FireCache.Clear();
        }

        public void OnClose()
        {
            FireCache.Clear();
            dic.Clear();
        }
    }
}