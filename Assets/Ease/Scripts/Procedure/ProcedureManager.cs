using Ease.Core;
using Ease.FSM;

namespace Ease.Procedure
{
    public class ProcedureManager : IProcedureManager
    {
        public Fsm<ProcedureManager> fsm;

        public ProcedureManager(params IProcedure[] procedures)
        {
            fsm = new Fsm<ProcedureManager>(this, procedures);
        }

        public void ChangeProcedure<TState>() where TState : IProcedure
        {
            fsm.ChangeState<TState>();
        }

        public void Start<TState>() where TState : IProcedure
        {
            fsm.Start<TState>();
        }

        public IProcedure CrrentProcedure => (IProcedure)fsm.CurrentState;

        public void OnUpdate(float time, float realtime)
        {
            fsm.OnUpdate(time, realtime);
        }

        public void OnClose()
        {
            fsm.OnClose();
        }
    }
}