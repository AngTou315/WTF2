using Ease.Sound;
using Third.Sound2;

namespace AAAShare.Adapter
{
    public class ThirdSoundManager2 : ISoundManager
    {
        public BSoundManager aSoundManager;

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