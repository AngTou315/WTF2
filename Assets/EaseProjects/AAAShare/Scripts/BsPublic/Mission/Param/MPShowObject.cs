using AAAShare.BsModules;
using AAAShare.BsModules.Agent;
using AAAShare.BsModules.Param;
using AAAShare.BsPublic.Agent;
using System.Collections.Generic;
using UnityEngine;

namespace AAAShare.BsPublic.Param
{
    public class MPShowObject : IMissionParam
    {
        public GameObject target;

        public string Des
        {
            get => "显示物体";
        }

        public IMissionAgent CreateAgent()
        {
            return new MAShowObject();
        }
    }
}