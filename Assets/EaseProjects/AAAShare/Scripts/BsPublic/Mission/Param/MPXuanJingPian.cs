using AAAShare.BsModules;
using AAAShare.BsModules.Agent;
using AAAShare.BsModules.Param;
using AAAShare.BsPublic.Agent;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

namespace AAAShare.BsPublic.Param
{
    public class MPXuanJingPian : IMissionParam
    {
        
        public UnityEvent<Action> evt;

        public string Des
        {
            get => "选镜片";
        }

        public IMissionAgent CreateAgent()
        {
            return new MAXuanJingPian();
        }
    }
}