using System;
using Ease.FSM;

namespace Ease.Procedure
{
    public interface IProcedure : IFsmState<ProcedureManager>
    {
    }

    public abstract class BaseProcedure : IProcedure
    {
        protected void ChangeProcedure<TState>(Fsm<ProcedureManager> fsm) where TState : IProcedure
        {
            fsm.ChangeState<TState>();
        }

        public abstract void OnEnter(Fsm<ProcedureManager> fsm);

        public abstract void OnQuit(Fsm<ProcedureManager> fsm);

        public abstract void OnUpdate(Fsm<ProcedureManager> fsm, float deltaTime, float realDeltaTime);
    }
}