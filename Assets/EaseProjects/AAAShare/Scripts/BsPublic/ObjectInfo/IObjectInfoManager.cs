using Ease.Core;
using UnityEngine;

namespace AAAShare.BsPublic.ObjectInfo
{
    /// <summary>
    /// 显示物品信息
    /// </summary>
    public interface IObjectInfoManager:IModule,ILife
    {
        public void Show(GameObject go);
        public void Hide(GameObject go);

        public void Register(ObjectInfoPool pool);
        public void UnRegister(ObjectInfoPool poo);
    }
}