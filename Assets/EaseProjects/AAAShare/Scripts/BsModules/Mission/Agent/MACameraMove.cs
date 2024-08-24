using System;
using AAAShare.BsModules.Param;
using AAAShare.BsPublic;
using Ease.UI;
using UnityEngine;
using Cinemachine;

namespace AAAShare.BsModules.Agent
{
    public class MACameraMove : IMissionAgent
    {
        public MissionData Data { get; set; }
        public event Action OnOVer;

        public void OnEnable()
        {
            var param = Data.Param as MPCameraMove;
            param.Vcam.gameObject.SetActive(true);
            OnOVer?.Invoke();
        }

        public void OnDisable()
        {
            Data = null;
        }

        public void OnUpdate()
        {
        }
    }
}