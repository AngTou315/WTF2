using Ease.Core;

namespace Ease.Version
{
    public interface IVersionManager: IModule
    {
        string GetVersion();
        public void SetVersionProvider(IVersionProvider versionProvider);
    }
}