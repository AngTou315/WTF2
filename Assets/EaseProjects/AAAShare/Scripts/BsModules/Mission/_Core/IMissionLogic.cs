using System;
using UnityEngine;

namespace AAAShare.BsModules
{
    /// <summary>
    /// 管理任务逻辑:
    /// </summary>
    public interface IMissionLogic
    {
        //当任务状态变化时触发的事件
        public event Action<MissionManagerState> OnStateChange;
        //其他变化时触发的事件。
        public event Action OnChange;

        //高亮
        //public event Action<GameObject> OnHightLightGameObject;

        //当前任务提示，可以读取或设置。
        public string currentMissionTip { get; set; }
    }
}