using AAAShare.BsModules.Agent;
using UnityEngine;
using Cinemachine;
namespace AAAShare.BsModules.Param
{
    /// <summary>
    /// 摄像机移动
    /// </summary>
    public class MPCameraMove : IMissionParam
    {
        public CinemachineVirtualCamera Vcam;

        public string Des { get=>"摄像机移动"; }

        public IMissionAgent CreateAgent()
        {
            return new MACameraMove();
        }
    }
}