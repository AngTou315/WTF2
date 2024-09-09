using AAAShare.BsPublic;
using AAAShare.BsPublic.Project;
using Ease.Event;
using Ease.FSM;
using Ease.Procedure;
using Ease.UI;
using EaseProjects.AAAShare.BsModules.Score;
using EaseProjects.Template.Event;

namespace EaseProjects.Template.Procedure
{
    /// <summary>
    /// 结算
    /// </summary>
    public class ProcedureSettlement : BaseProcedure
    {
        private bool backToLogin = false;

        public override void OnEnter(Fsm<ProcedureManager> fsm)
        {
            backToLogin = false;
            //根据难度弹出结算界面。 
            if (Entry.GetModule<IProjectManager>().difficulty == Difficulty.KaoHe)
            {
                Entry.GetModule<IEventManager>().Bind(SettleOverArgs.EventID, OnSettleOver);
                Entry.GetModule<IScoreManager>().Submit(OnSubmitStart, OnSubmitOver);
            }
            else
            {
                backToLogin = true;
            }
        }

        public override void OnQuit(Fsm<ProcedureManager> fsm)
        {
            if (Entry.GetModule<IProjectManager>().difficulty == Difficulty.KaoHe)
                Entry.GetModule<IEventManager>().UnBind(SettleOverArgs.EventID, OnSettleOver);
            backToLogin = false;
        }

        public override void OnUpdate(Fsm<ProcedureManager> fsm, float deltaTime, float realDeltaTime)
        {
            if (backToLogin)
            {
                fsm.SetData("newScene", "Login");
                Entry.GetModule<IUIManager>().CloseWindow("UIMain");
                ChangeProcedure<ProcedureChangeScene>(fsm);
            }
        }

        public void OnSubmitStart()
        {
            //Entry.GetModule<IUIManager>().OpenWindow("UITipLoading", new UITipLoadingParam() { title = "提交成绩中" });
        }

        public void OnSubmitOver(bool success)
        {
            Entry.GetModule<IUIManager>().CloseWindow("UITipLoading");
            Entry.GetModule<IUIManager>().OpenWindow("UISettlement", null);
        }

        public void OnSettleOver(object sender, BaseEventArgs eventArgs)
        {
            backToLogin = true;
        }
    }
}