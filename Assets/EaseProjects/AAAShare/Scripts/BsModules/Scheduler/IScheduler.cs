using System;
using System.Collections.Generic;
using Ease.Core;

namespace EaseProjects.AAAShare.BsModules.Scheduler
{
    /// <summary>
    /// 定时器
    /// </summary>
    public interface IScheduler : IModule, ILife
    {
        public void PostTask(Action task);
        public void Delay(Action task);
        public void Delay(Action task, float second, int repeatTime, Action Over);
        public void Delay(Action task, int frame, int repeatTime, Action Over);
    }
}