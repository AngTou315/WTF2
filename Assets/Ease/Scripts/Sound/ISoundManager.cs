using Ease.Core;

namespace Ease.Sound
{
    public interface ISoundManager: IModule
    {
        void Play(string name);
        void Stop(string name);
    }
}