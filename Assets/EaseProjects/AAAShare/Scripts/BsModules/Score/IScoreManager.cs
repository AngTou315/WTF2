using System;
using Ease.Core;

namespace EaseProjects.AAAShare.BsModules.Score
{
    public interface IScoreManager : IModule
    {
        public abstract void Submit(Action start, Action<bool> over);
    }
}