using System;
using AAAShare.BsPublic;
using Ease.Core;
using EaseProjects.AAAShare.BsModules.Scheduler;

namespace EaseProjects.AAAShare.BsModules.Score
{
    public class AIScoreManager : IScoreManager
    {
        private Action<bool> overCallback;

        public void Submit(Action start, Action<bool> over)
        {
            Entry.GetModule<IScheduler>().Delay(OnDelay, 3f, 1, null);
            start?.Invoke();
            overCallback = over;
        }

        private void OnDelay()
        {
            overCallback?.Invoke(true);
            overCallback = null;
        }
    }
}