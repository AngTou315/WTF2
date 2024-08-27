using System;
using Unity.VisualScripting;

namespace AAAShare.BsModules
{
    /// <summary>
    /// 任务执行者。作用：代替IMssion接口执行逻辑
    /// </summary>
    public interface IMissionAgent
    {
        MissionData Data { get; set; }

        public event Action OnOVer;

        public void OnEnable();
        public void OnDisable();
        public void OnUpdate();
    }
}