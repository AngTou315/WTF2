using System;
using AAAShare.BsModules;
using AAAShare.BsModules.Param;
using AAAShare.BsPublic.Param;
using EaseProjects.AAAShare.BsPublic.Component;

namespace AAAShare.BsPublic.Agent
{
    public class MAShowObject : IMissionAgent
    {
        public MissionData Data { get; set; }
        public event Action OnOVer;

        public void OnEnable()
        {
            var param = Data.Param as MPClickObject;
            param.target.gameObject.SetActive(true);
            OnOVer?.Invoke();
        }

        public void OnDisable()
        {
            Data = null;
            OnOVer = null;
        }

        public void OnUpdate()
        {
        }

    }
}