using AAAShare.BsPublic;
using AAAShare.BsPublic.Event;
using AAAShare.BsPublic.Expm;
using Ease.Core;
using Ease.Event;
using Ease.FSM;
using Ease.Procedure;
using Ease.UI;
using EaseProjects.Template.Event;
using UnityEngine;

namespace EaseProjects.Template.Procedure
{
    /// <summary>
    /// 手动点击退出/实验完成跳转至结算步骤
    /// </summary>
    public class ProcedureMain : BaseProcedure
    {
        private bool backToLogin = false;

        public override void OnEnter(Fsm<ProcedureManager> fsm)
        {
            backToLogin = false;
            Debug.Log( "OnEnter");
            //加载实验；
            Entry.GetModule<IExpmManager>().LoadExpm();
            //加载菜单
            Entry.GetModule<IUIManager>().OpenWindow("UIMain", null);
            Entry.GetModule<IEventManager>().Bind(BackEventArgs.EventID, OnBackToLogin);
            Entry.GetModule<IEventManager>().Bind(ExpmOverEventArgs.EventID, OnExpmOver);
        }

        public override void OnQuit(Fsm<ProcedureManager> fsm)
        {
            Entry.GetModule<IEventManager>().UnBind(BackEventArgs.EventID, OnBackToLogin);
            Entry.GetModule<IEventManager>().UnBind(ExpmOverEventArgs.EventID, OnExpmOver);
        }

        public override void OnUpdate(Fsm<ProcedureManager> fsm, float time, float realTime)
        {
            if (backToLogin)
            {
                //卸载Expm
                Entry.GetModule<IExpmManager>().UnLoadExpm();
                //跳转至结算结算
                ChangeProcedure<ProcedureSettlement>(fsm);
            }
        }

        public void OnBackToLogin(object sender, BaseEventArgs eventArgs)
        {
            backToLogin = true;
        }

        public void OnExpmOver(object sender, BaseEventArgs eventArgs)
        {
            backToLogin = true;
        }
    }
}