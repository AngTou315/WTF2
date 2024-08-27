using Ease.Core;
using UnityEngine;

namespace AAAShare.BsModules
{
    /// <summary>
    /// 负责高亮物体
    /// </summary>
    public interface IHightLightManager : IModule,ILife
    {
        //高亮
        public void Show(GameObject go);

        //取消高亮
        public void Hide(GameObject go);

        //取消所有
        public void HideAll();
    }
}