using System;
using AAAShare.BsModules;
using AAAShare.BsModules.Param;
using AAAShare.BsPublic.Param;
using EaseProjects.AAAShare.BsPublic.Component;

namespace AAAShare.BsPublic.Agent
{
    public class MAXuanJingPian : IMissionAgent
    {
        public MissionData Data { get; set; }
        public event Action OnOVer;

        public void OnEnable()
        {
            var param = Data.Param as MPXuanJingPian;
            OnOVer?.Invoke();
        }

        public void OnDisable()
        {
        }

        public void OnUpdate()
        {
        }
    }
}