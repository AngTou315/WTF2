using UnityEngine;

namespace EaseProjects.AAAShare.BsModules.Scheduler
{
    public class Scheduler : BaseScheduler
    {
        public Scheduler()
        {
            //Logger += Ease.Logger.Log;
            //LoggerError += Ease.Logger.LogError;
        }

        public override void OnClose()
        {
            base.OnClose();
            //Logger -= Ease.Logger.Log;
            //LoggerError -= Ease.Logger.LogError;
        }
    }
}