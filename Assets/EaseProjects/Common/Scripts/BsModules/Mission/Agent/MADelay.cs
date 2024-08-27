using System;
using AAAShare.BsModules.Param;
using UnityEngine;

namespace AAAShare.BsModules.Agent
{
    public class MADelay : IMissionAgent
    {
        public MissionData Data { get; set; }
        public event Action OnOVer;

        public float delayTime = 0f;
        public float usedTime = 0f;

        public void OnEnable()
        {
            MPDelay param = Data.Param as MPDelay;
            delayTime = param.delay;
            usedTime = 0f;
        }

        public void OnDisable()
        {
            usedTime = 0f;
        }

        public void OnUpdate()
        {
            if (usedTime <= delayTime)
            {
                usedTime += Time.deltaTime;
                if (usedTime >= delayTime)
                    OnOVer?.Invoke();
            }
        }
    }
}