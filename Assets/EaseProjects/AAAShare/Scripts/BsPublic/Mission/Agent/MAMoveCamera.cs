using System;
using AAAShare.BsModules;
using AAAShare.BsPublic.Param;
using UnityEngine;
using DG.Tweening;

namespace AAAShare.BsPublic.Agent
{
    public class MAMoveObj : IMissionAgent
    {
        public MissionData Data { get; set; }
        public event Action OnOVer;

        public void OnEnable()
        {
            var param = Data.Param as MPMoveObj;
            param.PropPos.DORotateQuaternion(param.targetPos.rotation, param.time);
            param.PropPos.DOMove(param.targetPos.position, param.time).OnComplete(() => OnOVer?.Invoke());
        }

        public void OnDisable()
        {
        }

        public void OnUpdate()
        {
            
        }
    }
}