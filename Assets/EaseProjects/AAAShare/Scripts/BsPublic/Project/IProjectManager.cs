using Ease.Core;
using UnityEngine;

namespace AAAShare.BsPublic.Project
{
    public interface IProjectManager : IModule
    {
        #region Data

        public Difficulty difficulty { get; set; }

        #endregion
    }
}