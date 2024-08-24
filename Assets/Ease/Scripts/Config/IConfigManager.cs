using Ease.Core;

namespace Ease.Config
{
    public interface IConfigManager : IModule
    {
        T GetConfig<T>() where T : class;
    }
}