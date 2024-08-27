using System;
using AAAShare.BsModules;
using AAAShare.BsPublic.Event;
using AAAShare.BsPublic.Param;
using Ease.Event;
using Ease.UI;
using EaseProjects.AAAShare.BsModules.Scheduler;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AAAShare.BsPublic.Agent
{
    public class MACountDown : IMissionAgent
    {
        public MissionData Data { get; set; }
        public event Action OnOVer;
        public int index = 0;

        public void OnEnable()
        {
            var param = Data.Param as MPCountDown;
            Entry.GetModule<IUIManager>().OpenWindow("UICountDown", new UICountDownParam() { title = param.tip, internalTime = param.internalTime, repeatTime = param.repeatTime });
            Entry.GetModule<IEventManager>().Bind(UICloseEventArgs.EventID, OnUIClose);
        }

        public void OnDisable()
        {
            Entry.GetModule<IEventManager>().UnBind(UICloseEventArgs.EventID, OnUIClose);
        }

        public void OnUpdate()
        {
        }

        private void OnUIClose(object sender, BaseEventArgs args)
        {
            if (args is UICloseEventArgs a && a.uiName == "UICountDown")
            {
                OnOVer?.Invoke();
            }
        }
    }
}