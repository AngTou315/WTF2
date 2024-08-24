using AAAShare.BsModules;
using AAAShare.BsModules.Agent;
using AAAShare.BsModules.Param;
using AAAShare.BsPublic.Agent;
using UnityEngine;
using UnityEngine.UI;

namespace AAAShare.BsPublic.Param
{
    public class MPObjSetActive : IMissionParam
    {
        public GameObject Prop;
        public bool isShow;

        public string Des
        {
            get => "显隐物体";
        }

        public IMissionAgent CreateAgent()
        {
            return new MAObjSetActive();
        }
    }
}