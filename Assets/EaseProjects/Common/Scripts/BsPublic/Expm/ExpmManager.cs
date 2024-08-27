using AAAShare.Adapter;
using AAAShare.BsModules;
using AAAShare.BsPublic.Event;
using Ease.Config;
using Ease.Event;
using Ease.UI;
using UnityEngine;

namespace AAAShare.BsPublic.Expm
{
    public class ExpmManager : IExpmManager
    {
        private GameObject expmGo = null;
        
        public IMissionLogic missionLogic { get; set; }

        public void LoadExpm()
        {
            var pjConfig = Entry.GetModule<IConfigManager>().GetConfig<ProjectConfig>();
            expmGo = GameObject.Instantiate(pjConfig.msPrefab);
            missionLogic = expmGo.GetComponentInChildren<IMissionLogic>();
            missionLogic.OnStateChange += OnStateChange;
            missionLogic.OnChange += OnChange;
        }

        public void UnLoadExpm()
        {
            missionLogic.OnStateChange -= OnStateChange;
            missionLogic.OnChange -= OnChange;
            missionLogic = null;
            expmGo = null;
        }

        private void OnStateChange(MissionManagerState state)
        {
            if (state == MissionManagerState.OVER)
            {
                var param = new UITipWindowParam();
                param.content = "实验结束，点击确认退出";
                param.callback1 = () =>
                {
                    Entry.GetModule<IUIManager>().CloseWindow("UITip");
                    Entry.GetModule<IEventManager>().FireNow(this, new ExpmOverEventArgs());
                };
                Entry.GetModule<IUIManager>().OpenWindow("UITip", param);
            }
        }

        private void OnChange()
        {
            Entry.GetModule<IEventManager>().FireNow(this, new ExpmUpdateEventArgs());
        }
    }
}