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
            Debug.Log("LaunchOnEnter");
        }

        public override void OnQuit(Fsm<ProcedureManager> fsm)
        {
            Debug.Log("LaunchOnQuit");
        }

        public override void OnUpdate(Fsm<ProcedureManager> fsm,float time,float realTime)
        {
            Debug.Log("OnUpdate" + index.ToString());
            index++;
            if (index > changeIndex)
            {
                fsm.SetData("newScene","Login");
                this.ChangeProcedure<ProcedureChangeScene>(fsm);
            }
        }
    }
}