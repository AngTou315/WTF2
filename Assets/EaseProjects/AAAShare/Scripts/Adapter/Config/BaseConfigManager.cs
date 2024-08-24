using Ease.Config;
using UnityEngine;
using UnityEngine.Serialization;

namespace AAAShare.Adapter
{
    public class BaseConfigManager : MonoBehaviour, IConfigManager
    {
        public ProjectConfig projectConfig;
        public virtual T GetConfig<T>() where T : class
        {
            if (typeof(T) == typeof(ProjectConfig))
                return projectConfig as T;
            return null;
        }
    }
}