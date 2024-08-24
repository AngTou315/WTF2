using System.Collections.Generic;
using AAAShare.BsPublic;
using AAAShare.BsPublic.Event;
using AAAShare.BsPublic.Expm;
using Ease.Event;
using UnityEngine;

namespace AAAShare.BsPublic.ObjectInfo
{
    public class DefaultObjectInfoManager : IObjectInfoManager
    {
        public Dictionary<GameObject, ObjectInfoPool> pools = new Dictionary<GameObject, ObjectInfoPool>();

        public void Show(GameObject go)
        {
            ObjectInfo info = null;
            foreach (var objectInfoPool in pools)
            {
                if (go.GetComponentInParent<ObjectInfoPool>().gameObject == objectInfoPool.Key)
                {
                    info = objectInfoPool.Value.GetInfo(go);
                }
            }

            if (info != null)
            {
                Ease.Logger.Log($"{GetType().Name} Show {go.name} {info.name} {info.des}");
                Entry.GetModule<IEventManager>().FireNow(this, new ObjectInfoEventArgs() { show = true, objectInfo = info });
            }
            else
                Ease.Logger.LogError($"{GetType().Name} Show {go.name} fail");
        }

        public void Hide(GameObject go)
        {
            ObjectInfo info = null;
            foreach (var objectInfoPool in pools)
            {
                if (go.GetComponentInParent<ObjectInfoPool>().gameObject == objectInfoPool.Key)
                {
                    info = objectInfoPool.Value.GetInfo(go);
                }
            }

            if (info != null)
            {
                Ease.Logger.Log($"{GetType().Name} Hide {go.name} {info.name} {info.des}");
                Entry.GetModule<IEventManager>().FireNow(this, new ObjectInfoEventArgs() { show = false, objectInfo = info });
            }
            else
                Ease.Logger.LogError($"{GetType().Name} Hide {go.name} fail");
        }

        public void Register(ObjectInfoPool pool)
        {
            if (!pools.ContainsKey(pool.gameObject))
            {
                pools.Add(pool.gameObject, pool);
            }
        }

        public void UnRegister(ObjectInfoPool pool)
        {
            pools.Remove(pool.gameObject);
        }

        public void OnUpdate(float time, float realtime)
        {
        }

        public void OnClose()
        {
            pools.Clear();
        }
    }
}