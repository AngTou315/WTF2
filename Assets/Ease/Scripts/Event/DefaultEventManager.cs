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
        public Dictionary<int, EventHandler<BaseEventArgs>> dic = new Dictionary<int, EventHandler<BaseEventArgs>>();
        public List<FireInfo> FireCache = new List<FireInfo>();

        public void Bind(int id, EventHandler<BaseEventArgs> callback)
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

        public void Fire(object sender, BaseEventArgs eventArgs)
        {
            if (FireCache.FirstOrDefault(x => x.sender == sender) != null)
                return;
            FireCache.Add(new FireInfo() { sender = sender, eventArgs = eventArgs });
        }

        public void FireNow(object sender, BaseEventArgs eventArgs)
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
                Fire(item.sender, item.eventArgs);
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