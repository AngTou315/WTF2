using AAAShare.BsModules;
using AAAShare.BsModules.Agent;
using AAAShare.BsModules.Param;
using AAAShare.BsPublic.Agent;
using UnityEngine;
using UnityEngine.UI;

namespace AAAShare.BsPublic.Param
{
    public class MPMoveObj : IMissionParam
    {
        public Transform PropPos;
        public Transform targetPos;
        public float time;

        public string Des
        {
            get => "移动物体";
        }

        public IMissionAgent CreateAgent()
        {
            return new MAMoveObj();
        }
    }
}