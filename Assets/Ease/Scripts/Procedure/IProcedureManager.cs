using Ease.Core;

namespace Ease.Procedure
{
    public interface IProcedureManager : IModule,ILife
    {
        void ChangeProcedure<TState>() where TState : IProcedure;

        void Start<TState>() where TState : IProcedure;
        public IProcedure CrrentProcedure { get; }
    }
}