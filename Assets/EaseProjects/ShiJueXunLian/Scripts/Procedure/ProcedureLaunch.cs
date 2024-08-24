using Ease.FSM;
using Ease.Procedure;
using UnityEngine;

namespace EaseProjects.Template.Procedure
{
    public class ProcedureLaunch : BaseProcedure
    {
        public int index = 0;
        public int changeIndex = 15;

        public override void OnEnter(Fsm<ProcedureManager> fsm)
        {
            Ease.Logger.Log(this,"OnEnter");
        }

        public override void OnQuit(Fsm<ProcedureManager> fsm)
        {
            Ease.Logger.Log(this,"OnQuit");

        }

        public override void OnUpdate(Fsm<ProcedureManager> fsm,float time,float realTime)
        {
            Ease.Logger.Log(this,"OnUpdate",index.ToString());
            index++;
            if (index > changeIndex)
            {
                fsm.SetData("newScene","Login");
                this.ChangeProcedure<ProcedureChangeScene>(fsm);
            }
        }
    }
}