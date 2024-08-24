using AAAShare.BsModules;
using AAAShare.BsModules.Agent;
using AAAShare.BsModules.Param;
using AAAShare.BsPublic.Agent;
using UnityEngine;
using UnityEngine.UI;

namespace AAAShare.BsPublic.Param
{
    public class MPDoScale : IMissionParam
    {
        public GameObject prop;
        public Vector3 v3;

        public float time;
        public string Des
        {
            get => "控制大小";
        }

        public IMissionAgent CreateAgent()
        {
            return new MADoScale();
        }
    }
}