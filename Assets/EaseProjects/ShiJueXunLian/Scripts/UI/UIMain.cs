using System;
using AAAShare.Adapter;
using AAAShare.BsPublic;
using AAAShare.BsPublic.Event;
using AAAShare.BsPublic.Expm;
using Ease.Core;
using Ease.Event;
using Ease.Procedure;
using Ease.UI;
using EaseProjects.Template.Event;
using UnityEngine;
using FW;
using TMPro;
using UnityEngine.EventSystems;

namespace EaseProjects.Template.UI
{
    public class UIMain : BaseUILogic
    {
        [SerializeField] private TextMeshProUGUI expmTip = null;
        [SerializeField] private TextMeshProUGUI objectInfo = null;

        protected override void OnOpen()
        {
            Entry.GetModule<IEventManager>().Bind(ExpmUpdateEventArgs.EventID, this.OnChange);
            Entry.GetModule<IEventManager>().Bind(ObjectInfoEventArgs.EventID, this.OnChange2);
        }

        protected override void OnClose()
        {
            Entry.GetModule<IEventManager>().UnBind(ExpmUpdateEventArgs.EventID, this.OnChange);
            Entry.GetModule<IEventManager>().UnBind(ObjectInfoEventArgs.EventID, this.OnChange2);
        }

        public void OnBackToLong()
        {
            var fwClass = new FwClass();
            var param = new UITipWindowParam();
            param.title = "提示";
            param.content = "返回登录界面？";
            param.callback1 = OnOK;
            param.callback2 = OnCancle;
            Entry.GetModule<IUIManager>().OpenWindow("UITip", param);
        }

        private void OnOK()
        {
            Entry.GetModule<IUIManager>().CloseWindow("UITip");
            Entry.GetModule<IEventManager>().FireNow(this, new BackEventArgs());
        }

        private void OnCancle()
        {
            Entry.GetModule<IUIManager>().CloseWindow("UITip");
        }

        private void OnChange(object sender, BaseEventArgs e)
        {
            if (e is ExpmUpdateEventArgs args)
            {
                var msTip = Entry.GetModule<IExpmManager>().missionLogic.currentMissionTip;
                expmTip.text = msTip;
            }
        }

        private void OnChange2(object sender, BaseEventArgs e)
        {
            if (e is ObjectInfoEventArgs args2)
            {
                if (args2.show)
                {
                    objectInfo.gameObject.SetActive(true);
                    objectInfo.text = args2.objectInfo.name;
                }
                else
                {
                    if (objectInfo != null)
                        objectInfo.gameObject.SetActive(false);
                }
            }
        }
    }
}