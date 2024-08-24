using System;
using AAAShare.BsModules;
using AAAShare.BsPublic.Param;
using UnityEngine;
using DG.Tweening;

namespace AAAShare.BsPublic.Agent
{
    public class MAObjSetActive : IMissionAgent
    {
        public MissionData Data { get; set; }
        public event Action OnOVer;

        public void OnEnable()
        {
            var param = Data.Param as MPObjSetActive;
            param.Prop.SetActive(param.isShow);
            if (param.Prop.activeSelf==param.isShow)
            {
                OnOVer?.Invoke();
            }
        }

        public void OnDisable()
        {
        }

        public void OnUpdate()
        {
            
        }
    }
}