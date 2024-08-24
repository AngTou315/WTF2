using Ease.Sound;
using Third.Sound;

namespace AAAShare.Adapter
{
    public class ThirdSoundManager : ISoundManager
    {
        public ASoundManager aSoundManager;

        public void Play(string name)
        {
            aSoundManager.Play(name);
        }

        public void Stop(string name)
        {
            aSoundManager.Stop(name);
        }
    }
}