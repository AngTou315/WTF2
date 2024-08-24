using System;
using AAAShare.BsModules.Param;
using AAAShare.BsPublic;
using Ease.UI;

namespace AAAShare.BsModules.Agent
{
    public class MAUITip : IMissionAgent
    {
        public MissionData Data { get; set; }
        public event Action OnOVer;

        public void OnEnable()
        {
            var param = Data.Param as MPUITip;
            var uiParam = new UITipWindowParam();
            uiParam.title = param.title;
            uiParam.content = param.content;
            uiParam.callback1 = () =>
            {
                Entry.GetModule<IUIManager>().CloseWindow("UITip");
                OnOVer?.Invoke();
            };
            Entry.GetModule<IUIManager>().OpenWindow("UITip", uiParam);
        }

        public void OnDisable()
        {
            Data = null;
        }

        public void OnUpdate()
        {
        }
    }
}