using System;
using Ease.Core;
using Object = UnityEngine.Object;

namespace Ease.Resource
{
    public interface IResourceManager : IModule,ILife
    {
        Object Load(string path);
        void LoadAsync(string path, Action start, Action<Object> end, Action<float> progress);
    }
}