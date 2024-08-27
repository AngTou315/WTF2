using AAAShare.BsPublic;
using Ease.Core;
using Ease.Event;
using Ease.FSM;
using Ease.Procedure;
using Ease.UI;
using EaseProjects.Template.Event;
using UnityEngine;

namespace EaseProjects.Template.Procedure
{
    public class ProcedureLogin : BaseProcedure
    {
        private bool _gotoMain = false;

        private bool gotoMain
        {
            get => _gotoMain;
            set
            {
                _gotoMain = value;
                Debug.Log( "goToMain" + _gotoMain.ToString());
            }
        }

        public override void OnEnter(Fsm<ProcedureManager> fsm)
        {
            gotoMain = false;
            Entry.GetModule<IUIManager>().OpenWindow("UILogin", null);
            Entry.GetModule<IEventManager>().Bind(ToMainEventArgs.EventID, this.OnEnterMain);
        }

        public override void OnQuit(Fsm<ProcedureManager> fsm)
        {
            Entry.GetModule<IEventManager>().UnBind(ToMainEventArgs.EventID, this.OnEnterMain);
            gotoMain = false;
        }

        public override void OnUpdate(Fsm<ProcedureManager> fsm, float time, float realTime)
        {
            if (gotoMain == true)
            {
                fsm.SetData("newScene", "Main");
                Entry.GetModule<IUIManager>().CloseWindow("UILogin");
                ChangeProcedure<ProcedureChangeScene>(fsm);
            }
        }

        public void OnEnterMain(object sender, BaseEventArgs args)
        {
            gotoMain = true;
        }
    }
}