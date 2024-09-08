﻿using System;
using System.Diagnostics;
using AAAShare.BsModules;
using AAAShare.BsModules.Param;
using AAAShare.BsPublic.Param;
using EaseProjects.AAAShare.BsPublic.Component;
using Unity.VisualScripting;
using UnityEngine;

namespace AAAShare.BsPublic.Agent
{
    public class MAXuanJingPian : IMissionAgent
    {
        public MissionData Data { get; set; }
        public event Action OnOVer;

        public void OnEnable()
        {
            var param = Data.Param as MPXuanJingPian;
            OnOVer += Ass;
            param.evt.Invoke(OnOVer);
        }

        public void OnDisable()
        {
        }

        public void OnUpdate()
        {
        }
        private void Ass()
        {
            UnityEngine.Debug.Log("哈哈哈哈哈");
        }
    }
}