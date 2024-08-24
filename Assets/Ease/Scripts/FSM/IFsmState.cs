namespace Ease.FSM
{
    public interface IFsmState<T>
    {
        void OnEnter(Fsm<T> fsm);
        void OnQuit(Fsm<T> fsm);
        void OnUpdate(Fsm<T> fsm,float deltaTime,float realDeltaTime);
    }
}