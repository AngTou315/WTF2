using Ease.Version;
using UnityEngine.Device;

namespace AAAShare.Adapter
{
    public class DefaultVersionProvider : IVersionProvider
    {
        public string Version
        {
            get => Application.version;
        }
    }
}