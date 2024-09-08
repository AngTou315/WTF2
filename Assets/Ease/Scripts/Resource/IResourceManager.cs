using System;
using Ease.Core;
using Object = UnityEngine.Object;

namespace Ease.Resource
{
    public interface IResourceManager : IModule,ILife
    {
        Object Load(string path);
        //该方法用于同步加载资源，unity自带的一个加载方法
        void LoadAsync(string path, Action start, Action<Object> end, Action<float> progress);
        //带着一个 Object 参数的原因是为了在资源异步加载完成后，将加载的资源对象传递给回调函数。
            //为什么异步加载
        //主线程会被阻塞，程序必须等待资源加载完成后才能继续执行其他操作。
        //这种方式可能导致程序的用户界面卡顿，尤其是在加载大型资源时，用户体验会变得很差。
    }
}