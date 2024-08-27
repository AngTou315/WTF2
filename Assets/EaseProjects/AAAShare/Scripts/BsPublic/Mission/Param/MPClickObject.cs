using AAAShare.BsModules;
using AAAShare.BsModules.Agent;
using AAAShare.BsModules.Param;
using AAAShare.BsPublic.Agent;
using UnityEngine;

namespace AAAShare.BsPublic.Param
{
    public class MPClickObject : IMissionParam
    {
        public GameObject target;

        public string Des
        {
            get => "ClickObject";
        }

        public IMissionAgent CreateAgent()
        {
            return new MAClickObject();
        }
    }
}