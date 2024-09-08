namespace Ease.Core
{
    public interface ILife
    {
        //用于更新对象的状态。
        void OnUpdate(float time, float realtime);
        //1当前帧的时间增量，2未受帧率影响的真实时间增量
        //以确保游戏逻辑或应用程序逻辑与帧率无关。

        //应用程序关闭时释放模块资源。
        void OnClose();
    }
}