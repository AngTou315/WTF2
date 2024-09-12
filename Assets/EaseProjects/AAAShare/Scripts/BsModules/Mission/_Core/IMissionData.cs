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
        public IMissionParam Param; //Param 会显示一个Params下拉选项，
        private static IEnumerable Params = MissionConst.GetAllParams();
        //IEnumerable是一个接口,用于表示可以枚举集合的类型
        //通过调用 MissionConst.GetAllParams() 来获取所有可用的 IMissionParam 子类实例，用于填充下拉列表。
    }

}