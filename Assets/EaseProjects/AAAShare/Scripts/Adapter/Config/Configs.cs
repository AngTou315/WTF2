using System;
using UnityEngine;

namespace AAAShare.Adapter
{
    [Serializable]
    public class BaseConfig
    {
        public string name;
    }

    [Serializable]
    public class ProjectConfig : BaseConfig
    {
        //名称
        public string title;

        //学校
        public string school;

        //实验预制体
        public GameObject msPrefab;
    }
}