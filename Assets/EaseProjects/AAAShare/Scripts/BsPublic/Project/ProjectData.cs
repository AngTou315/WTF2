using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AAAShare.BsPublic.Project
{
    /// <summary>
    /// 存放过程中的数据。
    /// </summary>
    [Serializable]
    public class ProjectData
    {
        [SerializeField] [ReadOnly] private Difficulty _difficulty;

        public Difficulty difficulty
        {
            get => _difficulty;
            set => _difficulty = value;
        }
    }
}