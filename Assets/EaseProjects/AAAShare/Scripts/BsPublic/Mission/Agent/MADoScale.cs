using System;
using AAAShare.BsModules;
using AAAShare.BsModules.Param;
using AAAShare.BsPublic.Param;
using EaseProjects.AAAShare.BsPublic.Component;
using DG.Tweening;

namespace AAAShare.BsPublic.Agent
{
    public class MADoScale : IMissionAgent
    {
        public MissionData Data { get; set; }
        public event Action OnOVer;

        public void OnEnable()
        {
            var param = Data.Param as MPDoScale;
            param.prop.transform.DOScale(param.v3, param.time).OnComplete(() =>
            {
                OnOVer?.Invoke();
            });
        }

        public void OnDisable()
        {

        }

        public void OnUpdate()
        {
        }
    }
}