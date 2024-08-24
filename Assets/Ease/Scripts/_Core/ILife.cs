namespace Ease.Core
{
    public interface ILife
    {
        void OnUpdate(float time, float realtime);

        void OnClose();
    }
}