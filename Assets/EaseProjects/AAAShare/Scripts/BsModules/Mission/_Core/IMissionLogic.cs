using System;
using UnityEngine;

namespace AAAShare.BsModules
{
    /// <summary>
    /// 管理任务逻辑:
    /// </summary>
    public interface IMissionLogic
    {
        //状态改变
        public event Action<MissionManagerState> OnStateChange;

        public event Action OnChange;

        //播放声音
        public event Action<String> OnPlaySound;

        //高亮
        public event Action<GameObject> OnHightLightGameObject;

        //log
        public event Action<string> Log;

        public event Action<string> LogError;

        //名称；
        public string name { get; set; }

        //当前步骤名称：
        public string currentMissionTip { get; set; }
    }
}