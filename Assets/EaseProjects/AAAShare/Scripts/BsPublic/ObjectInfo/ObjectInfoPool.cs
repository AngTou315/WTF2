using System;
using System.Collections.Generic;
using System.Linq;
using AAAShare.BsPublic;
using UnityEngine;

namespace AAAShare.BsPublic.ObjectInfo
{
    [Serializable]
    public class ObjectInfo
    {
        public GameObject key;
        public string name;
        public string des;
    }

    /// <summary>
    /// 物品信息池
    /// </summary>
    public class ObjectInfoPool : MonoBehaviour
    {
        public List<ObjectInfo> objectInfos = new List<ObjectInfo>();

        private void OnEnable()
        {
            Entry.GetModule<IObjectInfoManager>().Register(this);
        }

        private void OnDisable()
        {
            Entry.GetModule<IObjectInfoManager>()?.UnRegister(this);
        }

        public ObjectInfo GetInfo(GameObject go)
        {
            var target = objectInfos.FirstOrDefault(x => x.key == go);
            return target;
        }
    }
}