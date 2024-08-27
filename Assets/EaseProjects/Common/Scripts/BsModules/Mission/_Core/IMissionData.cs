using System;
using System.Collections;
using System.Collections.Generic;
using AAAShare.BsModules.Param;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AAAShare.BsModules
{
    /// <summary>
    /// 任务配置数据;
    /// </summary>
    public interface IMissionData
    {
    }

    [Serializable]
    public abstract class BaseMissionData : IMissionData
    {
        public string name;
        public int id;
    }

    [Serializable]
    public class MissionData : BaseMissionData
    {
        [SerializeReference] [ValueDropdown("Params")]
        public IMissionParam Param;
        private static IEnumerable Params = MissionConst.GetAllParams();
    }
    
}