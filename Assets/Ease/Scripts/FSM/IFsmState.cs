namespace Ease.FSM
{
    //状态机中状态的一个规定接口
    //IFsmState<T>接口定义了有限状态机（FSM）中每个状态的行为。
    //这是一个泛型接口，T表示状态机的所有者（通常是一些实体对象，如流程管理等）。
    //通过这个接口，状态机中的每个状态必须实现一些标准的生命周期方法。
    public interface IFsmState<T>
    {
        //状态进入
        void OnEnter(Fsm<T> fsm);
        void OnQuit(Fsm<T> fsm);
        //状态更新逻辑。
        void OnUpdate(Fsm<T> fsm,float deltaTime,float realDeltaTime);
    }
}